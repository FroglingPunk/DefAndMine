using Cysharp.Threading.Tasks;

public abstract class ControllerBase : IController
{
    public virtual async UniTask ConstructAsync()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask InitAsync()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask DeinitAsync()
    {
        await UniTask.Yield();
    }
}