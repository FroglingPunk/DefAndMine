﻿using System;
using UnityEngine;

public static class EDirectionExtensions
{
    private static readonly  int _eDirCount;

    static EDirectionExtensions()
    {
        _eDirCount = Enum.GetValues(typeof(EDirection)).Length;
    }


    public static Vector2Int ToDeltaField(this EDirection direction)
    {
        var delta = Vector2Int.zero;

        if (direction is EDirection.N or EDirection.NW or EDirection.NE)
        {
            delta.y = 1;
        }
        else if (direction is EDirection.S or EDirection.SW or EDirection.SE)
        {
            delta.y = -1;
        }

        if (direction is EDirection.E or EDirection.NE or EDirection.SE)
        {
            delta.x = 1;
        }
        else if (direction is EDirection.W or EDirection.NW or EDirection.SW)
        {
            delta.x = -1;
        }

        return delta;
    }

    public static EDirection Plus(this EDirection dir, int delta)
    {
        return (EDirection)(((int)dir + delta) % _eDirCount);
    }

    public static EDirection Opposite(this EDirection direction)
    {
        return (int)direction < 4 ? direction + 4 : direction - 4;
    }

    public static int Offset(this EDirection dir_1, EDirection dir_2)
    {
        return dir_1 > dir_2 ? dir_1 - dir_2 : dir_2 - dir_1;
    }

    public static EDirection Direction(this Cell from, Cell to)
    {
        if (from.Z == to.Z)
        {
            return to.X > from.X ? EDirection.E : EDirection.W;
        }

        if (from.X == to.X)
        {
            return to.Z > from.Z ? EDirection.N : EDirection.S;
        }

        var xOffset = to.X > from.X ? to.X - from.X : from.X - to.X;
        var zOffset = to.Z > from.Z ? to.Z - from.Z : from.Z - to.Z;

        var magnitude = UnityEngine.Mathf.Sqrt(xOffset * xOffset + zOffset * zOffset);
        var sin = zOffset / magnitude;

        if (to.X > from.X)
        {
            if (sin <= 0.5f)
            {
                return EDirection.E;
            }

            if (sin <= 0.70f)
            {
                return to.Z > from.Z ? EDirection.NE : EDirection.SE;
            }

            return to.Z > from.Z ? EDirection.E : EDirection.S;
        }

        if (sin <= 0.5f)
        {
            return EDirection.W;
        }

        if (sin <= 0.707f)
        {
            return to.Z > from.Z ? EDirection.NW : EDirection.SW;
        }

        return to.Z > from.Z ? EDirection.N : EDirection.S;
    }

    public static EDirection Next(this EDirection direction)
    {
        return direction == EDirection.NW ? EDirection.N : direction + 1;
    }

    public static EDirection Previous(this EDirection direction)
    {
        return direction == EDirection.N ? EDirection.NW : direction - 1;
    }

    public static EDirection Next2(this EDirection direction)
    {
        return direction > EDirection.SW ? direction - 6 : direction + 2;
    }

    public static EDirection Previous2(this EDirection direction)
    {
        return direction < EDirection.E ? direction + 6 : direction - 2;
    }
}