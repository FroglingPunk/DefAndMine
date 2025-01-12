using UnityEngine;

public static class CellExtensions
{
    public static Cell Neighbour(this Cell cell, EDirection direction)
    {
        var delta = direction.ToDeltaField();
        return Field.Instance[cell.PosX + delta.x, cell.PosZ + delta.y];
    }

    public static int Distance(this Cell from, Cell to)
    {
        return Mathf.Abs(from.PosX - to.PosX) + Mathf.Abs(from.PosZ - to.PosZ);
    }
}