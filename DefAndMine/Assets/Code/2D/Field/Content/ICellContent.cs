public interface ICellContent
{
    ECellContent Type { get; }
    Cell Cell { get; }
    bool IsSolid { get; }
    EDirection Direction { get; }
    EPowerTransitType PowerTransitType { get; }
    
    void Init(Cell cell, EDirection direction);
}