public static class CellExtensions
{
    public static Cell Neighbour(this Cell cell, EDirection direction)
    {
        var delta = direction.ToDeltaField();
        return Field.Instance[cell.PosX + delta.x, cell.PosZ + delta.y];
    }
}