using Cysharp.Threading.Tasks;

public abstract class SceneContainerBase
{
    public ControllersContainer ControllersContainer { get; protected set; } = new();
    public DataContainer DataContainer { get; protected set; } = new();


    public virtual async UniTask InitAsync()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask DeinitAsync()
    {
        await UniTask.Yield();
    }
}