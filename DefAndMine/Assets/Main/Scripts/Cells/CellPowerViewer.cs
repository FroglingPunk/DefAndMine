using UnityEngine;

public class CellPowerViewer : MonoBehaviour
{
    static public bool ShowIfCellPowerChanged = false;


    static public void ShowPower(Cell cell)
    {
        if (ShowIfCellPowerChanged)
        {
            TextMesh textMesh = cell.transform.GetChild(2).GetComponent<TextMesh>();

            if (cell.Wiring.Value)
            {
                if (cell.Power.Value)
                {
                    textMesh.color = Color.green;
                    textMesh.text = "][";
                }
                else
                {
                    textMesh.color = Color.red;
                    textMesh.text = "][";
                }
            }
            else
            {
                textMesh.text = string.Empty;
            }
        }
    }

    static public void HidePower(Cell cell)
    {
        TextMesh textMesh = cell.transform.GetChild(2).GetComponent<TextMesh>();
        textMesh.text = string.Empty;
    }
}