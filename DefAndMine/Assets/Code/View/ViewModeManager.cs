using UnityEngine;

public class ViewModeManager : MonoBehaviour
{
    [SerializeField] private EViewMode currentMode = EViewMode.Default;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChangeMode(EViewMode.Default);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChangeMode(EViewMode.Wiring);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ChangeMode(EViewMode.Power);
        }
    }


    private void ChangeMode(EViewMode mode)
    {
        if (mode == currentMode)
        {
            return;
        }

        EViewMode previousMode = currentMode;
        currentMode = mode;

        if (previousMode == EViewMode.Wiring)
        {
            // Field.Instance.ActionAllCells(CellWiringBuilder.HideWiring);
            // CellWiringBuilder.RebuildIfCellWiringChanged = false;
        }
        else if (previousMode == EViewMode.Power)
        {
            // Field.Instance.ActionAllCells(CellPowerViewer.HidePower);
            // CellPowerViewer.ShowIfCellPowerChanged = false;
        }

        if (currentMode == EViewMode.Wiring)
        {
            // CellWiringBuilder.RebuildIfCellWiringChanged = true;
            // Field.Instance.ActionAllCells(CellWiringBuilder.RebuildWiring);
        }
        else if (currentMode == EViewMode.Power)
        {
            // CellPowerViewer.ShowIfCellPowerChanged = true;
            // Field.Instance.ActionAllCells(CellPowerViewer.ShowPower);
        }
    }


    public enum EViewMode
    {
        Default,
        Wiring,
        Power
    }
}