using Cysharp.Threading.Tasks;

public class ControlPanel : UIControllerModel<ControlPanelView>
{
    private StructureBuildPanel _structureBuildPanel;
    private StructureInteractionPanel _structureInteractionPanel;


    public ControlPanel(ControlPanelView view) : base(view)
    {

    }

    public override UniTask InitAsync()
    {
        _structureBuildPanel = LocalSceneContainer.GetController<StructureBuildPanel>();
        _structureInteractionPanel = LocalSceneContainer.GetController<StructureInteractionPanel>();

        _view.ButtonBuildStructures.onClick.AddListener(OnButtonBuildStructureClick);

        LocalSceneContainer.MessageBus.Subscribe<SelectMessage<Cell>>(OnCellSelected);
        _view.gameObject.SetActive(true);
        return base.InitAsync();
    }

    public override UniTask DeinitAsync()
    {
        LocalSceneContainer.MessageBus.Unsubscribe<SelectMessage<Cell>>(OnCellSelected);
        return base.DeinitAsync();
    }

    private void OnButtonBuildStructureClick()
    {
        SetVisibilityState(false);
        _structureInteractionPanel.SetVisibilityState(false);
        _structureBuildPanel.SetVisibilityState(true);
    }

    private void OnCellSelected(SelectMessage<Cell> selectMessage)
    {
        if (!_view.gameObject.activeSelf)
        {
            return;
        }

        var cell = selectMessage.Selected;

        if (cell == null || cell.Structure.Value == null)
        {
            _structureInteractionPanel.SetVisibilityState(false);
            return;
        }

        _structureInteractionPanel.SetStructure(cell.Structure.Value);
        _structureInteractionPanel.SetVisibilityState(true);
    }
}