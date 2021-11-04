using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Room _startRoom;

    [SerializeField] private List<Room> _rooms;
    [SerializeField] private List<Corridor> _corridors;
    [SerializeField] private List<Junction> _junctions;

    private List<MapPiece> _allPieces = null;
    private List<MapPiece> AllPieces {
        get
        {
            return _allPieces ??= _junctions.Cast<MapPiece>()
                                     .Union(_corridors.Cast<MapPiece>())
                                     .Union(_rooms.Cast<MapPiece>())
                                     .ToList();
        }
    }

    [SerializeField] private int MaxDeepness = 4;
    [SerializeField] private float _roomSideLength;
    public float RoomSideLength => _roomSideLength;

    private Queue<MapPiece> _current_queue;
    private MapPiece _currentPiece;

    private Dictionary<Grid, MapPiece> _pieces;

    private void Start()
    {
        GenerateMap();
    }

    private void GenerateMap()
    {
        _pieces = new Dictionary<Grid, MapPiece>();
        Grid.SideLength = _roomSideLength;
        _current_queue = new Queue<MapPiece>();

        if (_rooms.Count == 0 || _corridors.Count == 0)
        {
            return;
        }

        _currentPiece = _startRoom.CreateCopyAt(Grid.Origin, 0, MapPieceRotation.None);
        _pieces[_currentPiece.Grid] = _currentPiece;


        while (_currentPiece.Deepness < MaxDeepness)
        {
            ConnectCurrentRoom();
            _currentPiece = _current_queue.Dequeue();
        }
    }

    private void ConnectCurrentRoom()
    {
        var grids = _currentPiece.GetNeighborsPotentialGridPosition();
        foreach (var grid in grids)
        {
            if (_pieces.ContainsKey(grid))
            {
                continue;
            }

            CreatePiece(grid);
        }
    }

    private void CreatePiece(Grid grid)
    {
        var triedPieces = new List<MapPiece>();

        List<MapPiece> neighbors = GetActiveNeighborsFrom(grid);

        bool ShouldIncludeRoom = neighbors.All((MapPiece piece) => piece.PieceType != MapPieceType.Room && piece.PieceType != MapPieceType.Junction);

        MapPiece nextPiece = GetRandomNextPieces(ShouldIncludeRoom, triedPieces);

        Side neighborsConnections = (Side)neighbors.Select(n => grid.GetSide(n.Grid, n.OpenSides))
                                                   .Cast<int>()
                                                   .Aggregate(0, (side, s) => side | s);

        
        for (var rotation = MapPieceRotation.None; rotation != MapPieceRotation.FullTurn; rotation = rotation.Next())
        {
            if ((rotation.RotateSides(nextPiece.OpenSides) & neighborsConnections) != neighborsConnections)
            {
                continue;
            }
            
            _current_queue.Enqueue(CreateNextPiece(grid, nextPiece, rotation));
            return;
        }

    }

    private MapPiece CreateNextPiece(Grid grid, MapPiece CopyPiece, MapPieceRotation rotation)
    {
        var piece = CopyPiece.CreateCopyAt(grid, _currentPiece.Deepness + 1, rotation); ;
        _pieces[piece.Grid] = piece;

        return _pieces[piece.Grid];
    }

    private List<MapPiece> GetActiveNeighborsFrom(Grid position)
    {
        return GetNeighborsGridFrom(position.GridPosition).Select( pos => new Grid(pos))
                                                          .Where(grid => _pieces.ContainsKey(grid))
                                                          .Select(grid => _pieces[grid])
                                                          .ToList();
    }

    public static List<Vector2Int> GetNeighborsGridFrom(Vector2Int position, Side side = Side.all)
    {
        return GetOutsets(Side.all).Select((Vector2Int outset_pos) => position + outset_pos).ToList();
    }

    public static List<Vector2Int> GetOutsets(Side openedSide)
    {
        List<Vector2Int> solutions = new List<Vector2Int>();

        if ((openedSide & Side.bot) != 0)
        {
            solutions.Add(new Vector2Int(0, -1));
        }

        if ((openedSide & Side.top) != 0)
        {
            solutions.Add(new Vector2Int(0, 1));
        }

        if ((openedSide & Side.left) != 0)
        {
            solutions.Add(new Vector2Int(-1, 0));
        }

        if ((openedSide & Side.right) != 0)
        {
            solutions.Add(new Vector2Int(1, 0));
        }

        return solutions;
    }

    private MapPiece GetRandomNextPieces(bool includesRooms, List<MapPiece> tried_pieces)
    {
        var all_potential_pieces = AllPieces.Where(piece => !tried_pieces.Contains(piece));

        if (!includesRooms)
        {
            all_potential_pieces = all_potential_pieces.Where( piece => piece.PieceType == MapPieceType.Corridor);
        }

        var list = all_potential_pieces.ToList();
            
        return list[Random.Range(0, list.Count)];
    }
}
