using System.Collections.Generic;
using UnityEngine;

public class Grid
{
    public Grid(Vector2Int grid_pos)
    {
        _gridPosition = grid_pos;
    }

    private Vector2Int _gridPosition;
    public Vector2Int GridPosition => _gridPosition;

    public Vector3 WorldPositon => new Vector3(_gridPosition.x * SideLength, 0, _gridPosition.y * SideLength);

    public static float SideLength;

    public static Grid Origin => new Grid(Vector2Int.zero);

    public Side GetSide(Grid neighbor, Side neighborOpenSides = Side.all)
    {
        var distance = neighbor.GridPosition - _gridPosition;
        if (distance == Vector2Int.left && (neighborOpenSides & Side.right) == Side.right)
        {
            return Side.left;
        }

        if (distance == Vector2Int.right && (neighborOpenSides & Side.left) == Side.left)
        {
            return Side.right;
        }

        if (distance == Vector2Int.up && (neighborOpenSides & Side.bot) == Side.bot)
        {
            return Side.top;
        }

        if (distance == Vector2Int.down && (neighborOpenSides & Side.top) == Side.top)
        {
            return Side.bot;
        }

        return new Side();
    }

    public override bool Equals(object obj)
    {
        return obj is Grid grid &&
               _gridPosition.Equals(grid._gridPosition);
    }

    public override int GetHashCode()
    {
        return 2002504201 + _gridPosition.GetHashCode();
    }

    public static bool operator == (Grid first, Grid second)
    {
        return first._gridPosition == second._gridPosition;
    }

    public static bool operator !=(Grid first, Grid second)
    {
        return first._gridPosition != second._gridPosition;
    }

    public class EqualityComparer : IEqualityComparer<Grid>
    {
        public bool Equals(Grid x, Grid y)
        {
            return x._gridPosition == y._gridPosition;
        }

        public int GetHashCode(Grid obj)
        {
            return obj._gridPosition.GetHashCode();
        }
    }
}
