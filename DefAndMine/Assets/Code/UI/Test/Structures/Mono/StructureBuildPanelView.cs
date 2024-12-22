using UnityEngine;
using UnityEngine.UI;

public class StructureBuildPanelView : UIControllerView
{
    [field:SerializeField] public ScrollRect ScrollRect { get; private set; }
    [field: SerializeField] public StructureBuildElementView ElementTemplate { get; private set; }
    [field: SerializeField] public Button ButtonBack { get; private set; }
    [field: SerializeField] public StructureData[] StructuresData { get; private set; }


    public override IUIControllerModel CreateModel()
    {
        return new StructureBuildPanel(this);
    }
}