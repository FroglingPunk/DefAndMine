using UnityEngine;

public class CellWiringBuilder : MonoBehaviour
{
    static public void RebuildWiring(Cell cell)
    {
        Transform cell_wiringX_trans = cell.transform.GetChild(0);
        Transform cell_wiringZ_trans = cell.transform.GetChild(1);

        if (cell.Wiring.Value)
        {
            bool[] neighborsHasWiring =
            {
                cell.HasNeighbor(EDirection.N) && cell.GetNeighbor(EDirection.N).Wiring.Value,
                cell.HasNeighbor(EDirection.E) && cell.GetNeighbor(EDirection.E).Wiring.Value,
                cell.HasNeighbor(EDirection.S) && cell.GetNeighbor(EDirection.S).Wiring.Value,
                cell.HasNeighbor(EDirection.W) && cell.GetNeighbor(EDirection.W).Wiring.Value
            };

            if (neighborsHasWiring[(int)EDirection.E] && neighborsHasWiring[(int)EDirection.W])
            {
                cell_wiringX_trans.localScale = new Vector3(1f, 0.1f, 0.1f);
                cell_wiringX_trans.localPosition = Vector3.zero;
            }
            else if (neighborsHasWiring[(int)EDirection.E])
            {
                cell_wiringX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                cell_wiringX_trans.localPosition = new Vector3(0.225f, 0f, 0f);
            }
            else if (neighborsHasWiring[(int)EDirection.W])
            {
                cell_wiringX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                cell_wiringX_trans.localPosition = new Vector3(-0.225f, 0f, 0f);
            }
            else
            {
                cell_wiringX_trans.localScale = new Vector3(0.3f, 0.1f, 0.3f);
                cell_wiringX_trans.localPosition = Vector3.zero;
            }

            if (neighborsHasWiring[(int)EDirection.N] && neighborsHasWiring[(int)EDirection.S])
            {
                cell_wiringZ_trans.localScale = new Vector3(1f, 0.1f, 0.1f);
                cell_wiringZ_trans.localPosition = Vector3.zero;
            }
            else if (neighborsHasWiring[(int)EDirection.N])
            {
                cell_wiringZ_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                cell_wiringZ_trans.localPosition = new Vector3(0f, 0f, 0.225f);
            }
            else if (neighborsHasWiring[(int)EDirection.S])
            {
                cell_wiringZ_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                cell_wiringZ_trans.localPosition = new Vector3(0f, 0f, -0.225f);
            }
            else
            {
                cell_wiringZ_trans.localScale = new Vector3(0.3f, 0.1f, 0.3f);
                cell_wiringZ_trans.localPosition = Vector3.zero;
            }
        }
        else
        {
            cell_wiringX_trans.localScale = Vector3.zero;
            cell_wiringZ_trans.localScale = Vector3.zero;
        }
    }
}