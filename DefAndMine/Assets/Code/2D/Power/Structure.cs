using UnityEngine;

public abstract class Structure : MonoBehaviour, IStructure
{
    public Cell Cell { get; private set; }
    public EDirection Direction { get; private set; } = EDirection.N;

    public virtual void Init(Cell cell)
    {
        Cell = cell;
        Cell.Structure = this;
        // transform.position = cell.transform.position;
    }

    public virtual void Demolish()
    {
        Cell.Structure = null;
        Destroy(gameObject);
    }

    public virtual void Rotate(bool clockwise)
    {
        transform.Rotate(0, 0, clockwise ? -90f : 90f);
        Direction = clockwise ? Direction.Next2() : Direction.Previous2();
    }
}