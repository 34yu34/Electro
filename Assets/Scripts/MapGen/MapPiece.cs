using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;


public abstract class MapPiece : MonoBehaviour
{

    [FormerlySerializedAs("_open_sides_mask")] [SerializeField] private Side _openSidesMask;
    [SerializeField] private MapRoomInteriorType _interiorType;
    
    public Side OpenSides => _rotation.RotateSides(_openSidesMask);

    public abstract MapPieceType PieceType { get; }

    public uint Deepness { get; private set; }
    private Grid _grid;
    private MapPieceRotation _rotation = MapPieceRotation.None;

    public Vector2Int GridPosition => _grid.GridPosition;
    public Grid Grid => _grid;

    public MapPiece CreateCopyAt(Grid grid, uint deepness, MapPieceRotation rotation)
    {
        var piece = Instantiate(this, grid.WorldPositon, rotation.Quaternion);
        piece._openSidesMask = _openSidesMask;
        piece.Deepness = deepness;
        piece._grid = grid;
        piece._rotation = rotation;
        return piece;
    }

    public void CreateEnemies(EnemiesGenerationData enemiesData)
    {
        if (_interiorType != MapRoomInteriorType.Enemies) return;
        
        var spawnSpot = GetRandomizedSpawnSpot();

        var chances = enemiesData.InitialSpawnChance;

        foreach(var spot in spawnSpot)
        {
            if (Random.Range(0f, 1f) < chances)
            {
                Instantiate(enemiesData.Enemies[0], spot.transform.position, Quaternion.identity);
            }
            chances -= enemiesData.SpawnChanceDropDown;
        }
    }
    

    public void CreatePowerUps(PickupsData pickupsData)
    {
        if (_interiorType != MapRoomInteriorType.PowerUp) return;

        var spawnSpot = GetRandomizedSpawnSpot();

        foreach (var spot in spawnSpot)
        {
            Instantiate(pickupsData.GetRandom(), spot.transform.position, Quaternion.identity);
        }
    }
    
    private IEnumerable<SpawnableSpot> GetRandomizedSpawnSpot()
    {
        IEnumerable<SpawnableSpot> spawnSpot = new List<SpawnableSpot>(GetComponentsInChildren<SpawnableSpot>());

        spawnSpot = Randomizer.Shuffle(spawnSpot);
        return spawnSpot;
    }
    
    private void Update()
    {
#if UNITY_EDITOR
        //if (_grid == null)
        //{
        //    return;
        //}
        //
        //var adjacent_grids = GetAdjacentLinkableGrid();
        //
        //foreach (var grid in adjacent_grids)
        //{
        //    var dir = grid.WorldPositon - transform.position;
        //    Debug.DrawLine(transform.position, transform.position + dir / 2);
        //}
#endif
    }

    public Side GetOpenedSideFor(Grid adjacentGrid)
    {
        var direction = Grid.GridPosition - adjacentGrid.GridPosition;

        var side = Side.GetSideFrom(direction);

        if (!HasSideOpen(side.Opposite()))
        {
            return Side.none;
        }

        return side;
    }

    public Side GetClosedSideFor(Grid adjacentGrid)
    {
        var direction = Grid.GridPosition - adjacentGrid.GridPosition;

        var side = Side.GetSideFrom(direction);

        if (!HasSideOpen(side.Opposite()))
        {
            return side;
        }

        return Side.none;
    }

    public bool HasSideOpen(Side side)
    {
        return (OpenSides & side) == side;
    }

    public List<Grid> GetAdjacentLinkableGrid()
    {
        return Grid.GetAdjacentGrid(OpenSides);
    }
}
