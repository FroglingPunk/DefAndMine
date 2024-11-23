using Cysharp.Threading.Tasks;
using UnityEngine;

public class InitSceneInitializer : SceneInitializerBase
{
    [SerializeField] private EScene _startScene = EScene.Init;


    public override async void Start()
    {
        var globalContainer = new GlobalContainer();
        await globalContainer.InitAsync();

        var localSceneContainer = new LocalSceneContainer(this);
        await localSceneContainer.InitAsync();

        SceneChanger.LoadScene(_startScene).Forget();
    }


    public override async UniTask InitAsync()
    {
        await UniTask.Yield();
    }

    public override async UniTask DeinitAsync()
    {
        await UniTask.Yield();
    }


    public static async UniTask DebugInitAsync()
    {
        var globalContainer = new GlobalContainer();
        await globalContainer.InitAsync();
    }
}