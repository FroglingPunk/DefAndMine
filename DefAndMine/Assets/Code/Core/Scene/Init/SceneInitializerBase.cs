using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class SceneInitializerBase : MonoBehaviour
{
    public virtual async void Start()
    {
#if UNITY_EDITOR
        if (GetType() != typeof(InitSceneInitializer) && GlobalContainer.Instance == null)
        {
            await InitSceneInitializer.DebugInitAsync();
        }
#endif
    }


    public abstract UniTask InitAsync();
    public abstract UniTask DeinitAsync();
}