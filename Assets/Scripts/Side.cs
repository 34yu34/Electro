using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Side
{
    [System.Flags]
    private enum SideEnum
    {
        back = 1 << 0,
        left = 1 << 1,
        front = 1 << 2,
        right = 1 << 3,
    }

    public static readonly string[] EnumOptions = { "back", "left", "front", "right"};

    public static Side none => new Side(0);
    public static Side back => new Side(SideEnum.back);
    public static Side left => new Side(SideEnum.left);
    public static Side front => new Side(SideEnum.front);
    public static Side right => new Side(SideEnum.right);
    public static Side all => new Side(SideEnum.back | SideEnum.left | SideEnum.front | SideEnum.right);
    
    public static readonly Side[] AllSides = { back, left, right, front };

    [SerializeField] private SideEnum _mask;

    private Side(SideEnum mask_value = 0)
    {
        _mask = mask_value;
    }

    private Side(int mask_value = 0)
    {
        _mask = (SideEnum)mask_value;
    }

    public Side(Side other)
    {
        _mask = other._mask;
    }

    public Side Opposite()
    {
        if (this == left)
        {
            return right;
        }

        if (this == right)
        {
            return left;
        }

        if (this == front)
        {
            return back;
        }

        if (this == back)
        {
            return front;
        }

        return none;
    }


    public Vector2Int GetDirection()
    {
        if (this & left)
        {
            return Vector2Int.left;
        }

        if (this & right)
        {
            return Vector2Int.right;
        }

        if (this & back)
        {
            return Vector2Int.down;
        }

        if (this & front)
        {
            return Vector2Int.up;
        }

        return Vector2Int.zero;
    }

    public static Side GetSideFrom(Vector2Int direction)
    {
        if (direction == Vector2Int.left)
        {
            return left;
        }

        if (direction == Vector2Int.right)
        {
            return right;
        }

        if (direction == Vector2Int.up)
        {
            return front;
        }

        if (direction == Vector2Int.down)
        {
            return back;
        }

        return none;
    }

    public static Side operator | (Side first, Side second)
    {
        return new Side(first._mask | second._mask);
    }

    public static Side operator & (Side first, Side second)
    {
        return new Side(first._mask & second._mask);
    }

    public static Side operator >> (Side first, int amount)
    {
        return new Side((int)first._mask >> amount);
    }

    public static Side operator << (Side first, int amount)
    {
        return new Side((int)first._mask << amount);
    }

    public static bool operator == (Side first, Side second)
    {
        return first._mask == second._mask;
    }

    public static bool operator != (Side first, Side second)
    {
        return first._mask != second._mask;
    }

    public static implicit operator int (Side side)
    {
        return (int)side._mask;
    }

    public static implicit operator bool (Side side)
    {
        return side._mask != 0;
    }

    public override bool Equals(object obj)
    {
        return _mask.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _mask.GetHashCode();
    }

    public override string ToString()
    {
        return _mask.ToString();
    }
}


