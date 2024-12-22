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
            
            var localSceneContainer = new LocalSceneContainer(this);
            await localSceneContainer.InitAsync();
        }
#endif
    }


    public abstract UniTask InitAsync();
    public abstract UniTask DeinitAsync();
}