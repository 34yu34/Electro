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
        initializeData();

        if (_rooms.Count == 0 || _corridors.Count == 0)
        {
            return;
        }

        placeStartPiece();

        placeConnectedPieces();
    }

    private void initializeData()
    {
        _pieces = new Dictionary<Grid, MapPiece>();
        Grid.SideLength = _roomSideLength;
        _current_queue = new Queue<MapPiece>();
    }

    private void placeStartPiece()
    {
        _currentPiece = _startRoom.CreateCopyAt(Grid.Origin, 0, MapPieceRotation.None);
        _pieces[_currentPiece.Grid] = _currentPiece;
        _current_queue.Enqueue(_currentPiece);
    }

    private void placeConnectedPieces()
    {
        while (_currentPiece.Deepness < MaxDeepness && _current_queue.Count > 0)
        {
            _currentPiece = _current_queue.Dequeue();
            ConnectCurrentRoom();
        }
    }

    private void ConnectCurrentRoom()
    {
        var grids = _currentPiece.GetAdjacentLinkableGrid();
        foreach (var grid in grids)
        {
            if (_pieces.ContainsKey(grid))
            {
                continue;
            }

            CreatePiece(grid);
        }
    }

    private void CreatePiece(Grid new_piece_grid)
    {
        List<MapPiece> neighbors = GetAdjacentMapPieceFrom(new_piece_grid);

        var future_piece = FuturMapPiece.createFuturMapPiece(neighbors, new_piece_grid);

        future_piece.CurrentTriedPiece = GetRandomNextPieces(future_piece);

        while (future_piece.CurrentTriedPiece != null && !tryAllPieceConfiguration(future_piece))
        {
            future_piece.TriedPieces.Add(future_piece.CurrentTriedPiece);
            future_piece.CurrentTriedPiece = GetRandomNextPieces(future_piece);
        }

    }

    private bool tryAllPieceConfiguration(FuturMapPiece futur_piece)
    {
        for (futur_piece.Rotation = MapPieceRotation.None; 
            futur_piece.Rotation != MapPieceRotation.FullTurn; 
            futur_piece.Rotation = futur_piece.Rotation.Next()
            )
        {
            if (!CanConnectSide(futur_piece))
            {
                continue;
            }

            _current_queue.Enqueue(CreateNextPiece(futur_piece));
            return true;
        }

        return false;
    }

    private bool CanConnectSide(FuturMapPiece futur_piece)
    {
        var rotationOpenedSide = futur_piece.Rotation.RotateSides(futur_piece.CurrentTriedPiece.OpenSides);

        if (_currentPiece.Deepness + 1 == MaxDeepness)
        {
            return (rotationOpenedSide & Side.all) == (futur_piece.SideToLink & Side.all);
        }

        return (rotationOpenedSide & futur_piece.SideToLink) == futur_piece.SideToLink &&
               (rotationOpenedSide & futur_piece.SideNotToLink) == Side.none;
    }

    private MapPiece CreateNextPiece(FuturMapPiece futur_piece)
    {
        var piece = futur_piece.CurrentTriedPiece.CreateCopyAt(futur_piece.Grid, _currentPiece.Deepness + 1, futur_piece.Rotation);
        _pieces[piece.Grid] = piece;

        return _pieces[piece.Grid];
    }

    private List<MapPiece> GetAdjacentMapPieceFrom(Grid position)
    {
        return position.GetAdjacentGrid().Where(grid => _pieces.ContainsKey(grid))
                                         .Select(grid => _pieces[grid])
                                         .ToList();
    }

    private MapPiece GetRandomNextPieces(FuturMapPiece future_piece)
    {
        var all_potential_pieces = AllPieces.Where(piece => !future_piece.TriedPieces.Contains(piece));

        if (!future_piece.ShouldIncludeRoom)
        {
            all_potential_pieces = all_potential_pieces.Where( piece => piece.PieceType == MapPieceType.Corridor);
        }

        var list = all_potential_pieces.ToList();

        if (list.Count == 0)
        {
            Debug.LogWarning($"No more pieces to add at {future_piece.Grid}");
            return null;
        }

            
        return list[Random.Range(0, list.Count)];
    }
}
