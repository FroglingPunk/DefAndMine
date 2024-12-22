using UnityEngine;
using UnityEngine.UI;

public class StructureInteractionPanelView : UIControllerView
{
    [field: SerializeField] public Button ButtonUpgrade { get; private set; }
    [field: SerializeField] public Button ButtonDestroy { get; private set; }
    [field: SerializeField] public Button ButtonRotateClockwise { get; private set; }
    [field: SerializeField] public Button ButtonRotateCounterClockwise { get; private set; }


    public override IUIControllerModel CreateModel()
    {
        return new StructureInteractionPanel(this);
    }
}