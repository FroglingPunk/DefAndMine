using UnityEngine;

public abstract class Structure : MonoBehaviour, IStructure
{
    public abstract ECellContent Type { get; }
    public bool IsSolid => true;
    public Cell Cell { get; private set; }
    public EDirection Direction { get; private set; } = EDirection.N;
    public abstract EPowerTransitType PowerTransitType { get; }

    public virtual void Init(Cell cell, EDirection direction)
    {
        Cell = cell;
        Cell.Content = this;
        Direction = direction;
        
        transform.position = cell.Transform.position;
        transform.eulerAngles = new Vector3(0, 45f * (int)direction, 0);
    }

    public virtual void Demolish()
    {
        Cell.Content = null;
        Destroy(gameObject);
    }

    public virtual void Rotate(bool clockwise)
    {
        transform.Rotate(0, 0, clockwise ? -90f : 90f);
        Direction = clockwise ? Direction.Next2() : Direction.Previous2();
    }
}