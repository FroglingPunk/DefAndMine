using UnityEngine;
using UnityEngine.UI;

public class ControlPanelView : UIControllerView
{
    [field: SerializeField] public Button ButtonBuildStructures { get; private set; }


    public override IUIControllerModel CreateModel()
    {
        return new ControlPanel(this);
    }
}