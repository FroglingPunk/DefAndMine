using UnityEngine;

public class CellWiringBuilder
{
    static public bool RebuildIfCellWiringChanged = false;


    static public void RebuildWiring(Cell cell)
    {
        if (RebuildIfCellWiringChanged)
        {
            Transform cell_wiringX_trans = cell.transform.GetChild(0);
            Transform cell_wiringZ_trans = cell.transform.GetChild(1);

            cell_wiringX_trans.gameObject.SetActive(true);
            cell_wiringZ_trans.gameObject.SetActive(true);

            if (cell.Wiring.Value)
            {
                bool[] neighborsHasWiring =
                {
                cell.NeighborHasWiring(EDirection.N),
                cell.NeighborHasWiring(EDirection.E),
                cell.NeighborHasWiring(EDirection.S),
                cell.NeighborHasWiring(EDirection.W)
            };

                if (neighborsHasWiring[(int)EDirection.E / 2] && neighborsHasWiring[(int)EDirection.W / 2])
                {
                    cell_wiringX_trans.localScale = new Vector3(1f, 0.1f, 0.1f);
                    cell_wiringX_trans.localPosition = Vector3.zero;
                }
                else if (neighborsHasWiring[(int)EDirection.E / 2])
                {
                    cell_wiringX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                    cell_wiringX_trans.localPosition = new Vector3(0.225f, 0f, 0f);
                }
                else if (neighborsHasWiring[(int)EDirection.W / 2])
                {
                    cell_wiringX_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                    cell_wiringX_trans.localPosition = new Vector3(-0.225f, 0f, 0f);
                }
                else
                {
                    cell_wiringX_trans.localScale = new Vector3(0.3f, 0.1f, 0.3f);
                    cell_wiringX_trans.localPosition = Vector3.zero;
                }

                if (neighborsHasWiring[(int)EDirection.N / 2] && neighborsHasWiring[(int)EDirection.S / 2])
                {
                    cell_wiringZ_trans.localScale = new Vector3(1f, 0.1f, 0.1f);
                    cell_wiringZ_trans.localPosition = Vector3.zero;
                }
                else if (neighborsHasWiring[(int)EDirection.N / 2])
                {
                    cell_wiringZ_trans.localScale = new Vector3(0.55f, 0.1f, 0.1f);
                    cell_wiringZ_trans.localPosition = new Vector3(0f, 0f, 0.225f);
                }
                else if (neighborsHasWiring[(int)EDirection.S / 2])
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

    static public void HideWiring(Cell cell)
    {
        cell.transform.GetChild(0).gameObject.SetActive(false);
        cell.transform.GetChild(1).gameObject.SetActive(false);
    }
}