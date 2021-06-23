using UnityEngine;
using Constructor.Structures;

public class CellWiring : NotifyingVariable<bool> { }
public class CellStructure : NotifyingVariable<Structure> { }
public class CellPower : NotifyingVariable<bool> { }

public class Cell : MonoBehaviour
{
    public CellWiring Wiring { get; private set; }
    public CellPower Power { get; private set; }
    public int XId { get; private set; }
    public int ZId { get; private set; }
    public CellStructure Structure { get; set; }
    public EPlace Place { get; private set; }


    public void Init(int id_x, int id_z)
    {
        XId = id_x;
        ZId = id_z;

        Wiring = new CellWiring();/*{ Value = false };*/
        Power = new CellPower { Value = false };
        Structure = new CellStructure();
        Place = (Mathf.Abs(transform.localPosition.y) < 0.1f ? EPlace.Ground : EPlace.Elevation);

        Wiring.OnValueChanged += (value) => CellWiringBuilder.RebuildWiring(this);
        Wiring.Value = false;

        for (EDirection direction = EDirection.S; direction < EDirection.NW; direction += 2)
        {
            Cell neighbor = GetNeighbor(direction);
            if (neighbor != null)
            {
                neighbor.Wiring.OnValueChanged += (value) => CellWiringBuilder.RebuildWiring(this);
                Wiring.OnValueChanged += (value) => CellWiringBuilder.RebuildWiring(neighbor);
            }
        }

        Structure.OnValueChanged += (value) => CellPowerSpreader.Spread();
        Wiring.OnValueChanged += (value) => CellPowerSpreader.Spread();
        Power.OnValueChanged += (value) => CellPowerViewer.ShowPower(this);
    }

    public Cell GetNeighbor(EDirection direction)
    {
        int neighborXID = XId;
        int neighborZID = ZId;

        if (direction > EDirection.N && direction < EDirection.S)
        {
            neighborXID++;
        }
        else if (direction > EDirection.S)
        {
            neighborXID--;
        }

        if (direction < EDirection.E || direction > EDirection.W)
        {
            neighborZID++;
        }
        else if (direction > EDirection.E && direction < EDirection.W)
        {
            neighborZID--;
        }

        return Field.Instance[neighborXID, neighborZID];
    }

    public bool NeighborHasWiring(EDirection direction)
    {
        Cell neighbor = GetNeighbor(direction);
        return neighbor != null && neighbor.Wiring.Value;
    }
}