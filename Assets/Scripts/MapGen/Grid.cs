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

    public static float SideLength = 10;

    public static Grid Origin => new Grid(Vector2Int.zero);

    public Grid FindInDirection(Vector2Int distance)
    {
        return new Grid(_gridPosition + distance);
    }

    public List<Grid> GetAdjacentGrid(Side SideFilter = null)
    {
        SideFilter ??= Side.all;

        List<Grid> solutions = new List<Grid>();

        if ((SideFilter & Side.back) != 0)
        {
            solutions.Add(FindInDirection(Vector2Int.down));
        }

        if ((SideFilter & Side.front) != 0)
        {
            solutions.Add(FindInDirection(Vector2Int.up));
        }

        if ((SideFilter & Side.left) != 0)
        {
            solutions.Add(FindInDirection(Vector2Int.left));
        }

        if ((SideFilter & Side.right) != 0)
        {
            solutions.Add(FindInDirection(Vector2Int.right));
        }

        return solutions;
    }

    public override string ToString()
    {
        return _gridPosition.ToString();
    }

    // DICTIONNARY INTERFACE

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
