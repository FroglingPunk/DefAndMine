using Cysharp.Threading.Tasks;
using UniRx;

public abstract class ModulesHandlerBase : ControllerBase
{
    private CompositeDisposable _disposables = new();

    public override UniTask InitAsync()
    {
        var structuresController = LocalSceneContainer.GetController<StructuresController>();

        _disposables.Add(structuresController.Structures.ObserveAdd()
            .Subscribe(obs => OnStructureAdded(obs.Value)));

        _disposables.Add(structuresController.Structures.ObserveRemove()
            .Subscribe(obs => OnStructureRemoved(obs.Value)));

        return base.InitAsync();
    }

    public override UniTask DeinitAsync()
    {
        _disposables?.Dispose();
        return base.DeinitAsync();
    }


    protected abstract void OnStructureAdded(StructureBase structure);
    protected abstract void OnStructureRemoved(StructureBase structure);
}