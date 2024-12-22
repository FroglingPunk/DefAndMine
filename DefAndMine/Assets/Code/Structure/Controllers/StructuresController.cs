using Cysharp.Threading.Tasks;
using UniRx;

public class StructuresController : ControllerBase
{
    public IReadOnlyReactiveCollection<StructureBase> Structures => _structures;

    private readonly ReactiveCollection<StructureBase> _structures = new();


    public override UniTask InitAsync()
    {
        LocalSceneContainer.MessageBus.Subscribe<BuildStructureMessage>(OnStructureBuilded);

        return base.InitAsync();
    }

    public override UniTask DeinitAsync()
    {
        LocalSceneContainer.MessageBus.Unsubscribe<BuildStructureMessage>(OnStructureBuilded);

        return base.DeinitAsync();
    }


    private void OnStructureBuilded(BuildStructureMessage message)
    {
        var structure = message.Structure;
        _structures.Add(structure);
    }
}