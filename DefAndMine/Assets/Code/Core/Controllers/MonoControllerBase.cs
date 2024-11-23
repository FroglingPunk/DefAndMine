using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class MonoControllerBase : MonoBehaviour, IController
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