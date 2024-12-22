using Cysharp.Threading.Tasks;

public class StructureInteractionPanel : UIControllerModel<StructureInteractionPanelView>
{
    private StructureBase _structure;


    public StructureInteractionPanel(StructureInteractionPanelView view) : base(view)
    {
    }

    public override UniTask InitAsync()
    {
        _view.ButtonRotateClockwise.onClick.AddListener(() => _structure?.Rotate(true));
        _view.ButtonRotateCounterClockwise.onClick.AddListener(() => _structure?.Rotate(false));
        _view.ButtonUpgrade.onClick.AddListener(() => _structure?.Upgrade());
        _view.ButtonDestroy.onClick.AddListener(() => _structure?.Destroy());

        return base.InitAsync();
    }


    public void SetStructure(StructureBase structure)
    {
        _structure = structure;
    }
}