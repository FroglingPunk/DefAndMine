using UnityEngine;

public class Cell : MonoBehaviour
{
    public CellWiring Wiring { get; private set; }
    public CellPower Power { get; private set; }
    public Field Field { get; private set; }
    public int IDx { get; private set; }
    public int IDz { get; private set; }

    private Cell[] neighbors;


    public void Init(Field field, int id_x, int id_z)
    {
        Field = field;
        IDx = id_x;
        IDz = id_z;

        neighbors = new Cell[4];
        Wiring = new CellWiring();
        Power = new CellPower();

        Wiring.OnValueChanged += (value) => CellWiringBuilder.RebuildWiring(this);
        Wiring.Value = false;

        Power.Value = 0;
    }

    public bool HasNeighbor(EDirection direction)
    {
        return neighbors[(int)direction] != null;
    }

    public Cell GetNeighbor(EDirection direction)
    {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(Cell neighbor, EDirection direction)
    {
        neighbors[(int)direction] = neighbor;
        neighbor.neighbors[(int)direction.Opposite()] = this;

        neighbor.Wiring.OnValueChanged += (value) => CellWiringBuilder.RebuildWiring(this);
        Wiring.OnValueChanged += (value) => CellWiringBuilder.RebuildWiring(neighbor);
    }
}