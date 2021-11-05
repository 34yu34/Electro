using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class MapPiece : MonoBehaviour
{

    [SerializeField] private Side _open_sides_mask;
    public Side OpenSides => _rotation.RotateSides(_open_sides_mask);

    public abstract MapPieceType PieceType { get; }

    public uint Deepness { get; private set; }
    private Grid _grid;
    private MapPieceRotation _rotation = MapPieceRotation.None;

    public Vector2Int GridPosition => _grid.GridPosition;
    public Grid Grid => _grid;

    public MapPiece CreateCopyAt(Grid grid, uint deepness, MapPieceRotation rotation)
    {
        var piece = Instantiate(this, grid.WorldPositon, rotation.Quaternion);
        piece._open_sides_mask = _open_sides_mask;
        piece.Deepness = deepness;
        piece._grid = grid;
        piece._rotation = rotation;
        return piece;
    }

    private void Update()
    {
        var adjacent_grids = GetAdjacentLinkableGrid();

        foreach (var grid in adjacent_grids)
        {
            var dir = grid.WorldPositon - transform.position;
            Debug.DrawLine(transform.position, transform.position + dir / 2);
        }
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
