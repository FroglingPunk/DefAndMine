public interface ICell
{
    int PosX { get; }
    int PosZ { get; }

    ECellType Type { get; }
    ECellHeight Height { get; }
    ICellContent Content { get; }
}