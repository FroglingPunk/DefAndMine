using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class GameSceneInitializer : SceneInitializerBase
{
    [SerializeField] private List<MonoControllerBase> _monoControllers;


    private void Reset()
    {
        _monoControllers = new(FindObjectsOfType<MonoControllerBase>());
    }


    public override async UniTask InitAsync()
    {
        _monoControllers.ForEach(mc =>
            LocalSceneContainer.Instance.ControllersContainer.AddControllerAs(mc, mc.GetType()));

        var controllers = CreateControllers();
        controllers.AddRange(_monoControllers);

        var constructTasks = new List<UniTask>();
        var initTasks = new List<UniTask>();

        foreach (var controller in controllers)
        {
            constructTasks.Add(controller.ConstructAsync());
        }

        await UniTask.WhenAll(constructTasks);

        foreach (var controller in controllers)
        {
            initTasks.Add(controller.InitAsync());
        }

        await UniTask.WhenAll(initTasks);
    }


    public override async UniTask DeinitAsync()
    {
        await UniTask.Yield();
    }


    private List<IController> CreateControllers()
    {
        var controllersContainer = LocalSceneContainer.Instance.ControllersContainer;

        return new List<IController>()
        {
            // controllersContainer.AddController(new ObserverController())
        };
    }
}