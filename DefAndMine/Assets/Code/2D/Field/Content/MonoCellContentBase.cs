using UnityEngine;

public abstract class MonoCellContentBase : MonoBehaviour, ICellContent
{
    public abstract ECellContent Type { get; }
    public Cell Cell { get; private set; }
    public abstract bool IsSolid { get; }
    public virtual EPowerTransitType PowerTransitType => EPowerTransitType.None;
    public EDirection Direction { get; private set; } = EDirection.N;
    

    public virtual void Init(Cell cell, EDirection direction)
    {
        Cell = cell;
        Direction = direction;
        
        transform.position = cell.Transform.position;
        transform.eulerAngles = new Vector3(0, 45f * (int)direction, 0);
    }
}