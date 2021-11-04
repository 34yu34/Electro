using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public abstract class MapPiece : MonoBehaviour
{

    [SerializeField] [EnumMask] private Side _open_sides_mask;
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
        piece.Deepness = deepness;
        piece._grid = grid;
        piece._rotation = rotation;
        return piece;
    }

    public List<Grid> GetNeighborsPotentialGridPosition()
    {
        return MapGenerator.GetOutsets(_rotation.RotateSides(_open_sides_mask)).Select( pos => new Grid(GridPosition + pos)).ToList();
    }

    private void Update()
    {
        var sides = MapGenerator.GetOutsets(_rotation.RotateSides(_open_sides_mask));

        foreach (var side in sides)
        {
            Debug.DrawLine(transform.position, transform.position + new Vector3(side.x, 0, side.y));
        }
    }
}
