using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapPiecesGroup _mapPieceData;
    [SerializeField] private PickupsData _pickupsData;
    
    private EnemiesGenerationData _enemiesData;
    private bool EndRoomPlaced { get; set; }

    [FormerlySerializedAs("MaxDeepness")] [SerializeField] private int _maxDeepness = 4;
    [SerializeField] private float _roomSideLength;

    private const int START_DEEPNESS = 0;

    public float RoomSideLength => _roomSideLength;

    private Queue<MapPiece> _currentQueue;
    private MapPiece _currentPiece;
    private Dictionary<Grid, MapPiece> _pieces;

    public void GenerateMap(EnemiesGenerationData enemiesData)
    {
        InitializeData(enemiesData);

        _mapPieceData.AssertData();

        PlaceStartPiece();

        PlaceConnectedPieces();

        AddEnemies();
        
        AddPowerUps();
    }

    private void InitializeData(EnemiesGenerationData enemies)
    {
        _enemiesData = enemies;
        _pieces = new Dictionary<Grid, MapPiece>();
        Grid.SideLength = _roomSideLength;
        _currentQueue = new Queue<MapPiece>();
        EndRoomPlaced = false;
    }

    private void PlaceStartPiece()
    {
        var startPiece = _mapPieceData.StartRoom.CreateCopyAt(Grid.Origin, START_DEEPNESS, MapPieceRotation.None);
        _pieces[startPiece.Grid] = startPiece;
        _currentPiece = startPiece;
        _currentQueue.Enqueue(startPiece);
    }

    private void AddEnemies()
    {
        foreach (var piece in _pieces)
        {
            piece.Value.CreateEnemies(_enemiesData);
        }
    }

    private void AddPowerUps()
    {
        foreach (var piece in _pieces)   
        {
            piece.Value.CreatePowerUps(_pickupsData);
        }
    }

    private void PlaceConnectedPieces()
    {
        while (_currentPiece.Deepness < _maxDeepness && _currentQueue.Count > 0)
        {
            _currentPiece = _currentQueue.Dequeue();
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

            AddPieceAtGrid(grid);
        }
    }

    private void AddPieceAtGrid(Grid newPieceGrid)
    {
        List<MapPiece> neighbors = GetAdjacentMapPieceFrom(newPieceGrid);

        var futurePiece = FuturMapPiece.CreateFutureMapPiece(neighbors, newPieceGrid, IsLastDeepnessLevel());


        if (!futurePiece.TryAllPieces(_mapPieceData, ShouldTryEndRoom()))
        {
            Debug.LogError($"impossible to place pieces at grid {newPieceGrid}");
            return;
        }

        _currentQueue.Enqueue(CreateNextPiece(futurePiece));
    }

    private MapPiece CreateNextPiece(FuturMapPiece futurPiece)
    {
        ShouldSetEndRoomPlaced(futurPiece);

        var piece = futurPiece.CurrentPiece.CreateCopyAt(futurPiece.Grid, _currentPiece.Deepness + 1, futurPiece.Rotation);
        _pieces[piece.Grid] = piece;

        return _pieces[piece.Grid];
    }

    private void ShouldSetEndRoomPlaced(FuturMapPiece futurPiece)
    {
        if (futurPiece.CurrentPiece != _mapPieceData.EndRoom)
        {
            return;
        }

        EndRoomPlaced = true;
    }

    private List<MapPiece> GetAdjacentMapPieceFrom(Grid position)
    {
        return position.GetAdjacentGrid().Where(grid => _pieces.ContainsKey(grid))
                                         .Select(grid => _pieces[grid])
                                         .ToList();
    }

    private bool ShouldTryEndRoom()
    {
        return IsLastDeepnessLevel() && !EndRoomPlaced;
    }

    private bool IsLastDeepnessLevel()
    {
        return _currentPiece.Deepness + 1 == _maxDeepness;
    }
}
