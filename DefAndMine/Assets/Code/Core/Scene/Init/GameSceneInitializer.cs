using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSceneInitializer : SceneInitializerBase
{
    [SerializeField] private List<InstallerBase> _installers;


    private void Reset()
    {
        _installers = GetComponentsInChildren<InstallerBase>(true).ToList();
    }


    public override async UniTask InitAsync()
    {
        var controllers = new List<IController>();
        _installers.ForEach(installer => controllers.AddRange(installer.RegisterControllers()));

        var constructTasks = new List<UniTask>();
        controllers.ForEach(controller => constructTasks.Add(controller.ConstructAsync()));
        await UniTask.WhenAll(constructTasks);

        var initTasks = new List<UniTask>();
        controllers.ForEach(controller => initTasks.Add(controller.InitAsync()));
        await UniTask.WhenAll(initTasks);
    }


    public override async UniTask DeinitAsync()
    {
        await UniTask.Yield();
    }
}