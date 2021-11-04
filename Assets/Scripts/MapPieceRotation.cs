using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPieceRotation
{
    private uint _rotation_level;

    private const uint MAX_ROTATION = 4;

    public static MapPieceRotation None => new MapPieceRotation(0);
    public static MapPieceRotation Quarter => new MapPieceRotation(1);
    public static MapPieceRotation Half => new MapPieceRotation(2);
    public static MapPieceRotation ThreeQuarter => new MapPieceRotation(3);
    public static MapPieceRotation FullTurn => new MapPieceRotation(4);


    private MapPieceRotation(uint rotation_level = 0)
    {
        _rotation_level = rotation_level;
    }

    public MapPieceRotation Next()
    {
        return new MapPieceRotation(_rotation_level + 1);
    }

    public Quaternion Quaternion
    {
        get
        {
            return Quaternion.AngleAxis(90 * _rotation_level, Vector3.up);
        }
    }

    public Side RotateSides(Side bit_mask)
    {
        return (Side)
        (
                ((int)bit_mask <<                      (int)_rotation_level) | 
                ((int)bit_mask >> ((int)MAX_ROTATION - (int)_rotation_level))
        );
    }

    public static bool operator == (MapPieceRotation first, MapPieceRotation second)
    {
        return first._rotation_level == second._rotation_level;
    }

    public static bool operator !=(MapPieceRotation first, MapPieceRotation second)
    {
        return first._rotation_level != second._rotation_level;
    }

    public override bool Equals(object obj)
    {
        return _rotation_level.Equals(obj);
    }

    public override int GetHashCode()
    {
        return _rotation_level.GetHashCode();
    }
}
