using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class GlobalContainer : SceneContainerBase
{
    public static GlobalContainer Instance { get; private set; }

    public static MessageBus MessageBus => Instance.ControllersContainer.GetController<MessageBus>();

    public static T GetController<T>() where T : class, IController
    {
        return Instance.ControllersContainer.GetController<T>();
    }


    public override async UniTask InitAsync()
    {
        await UniTask.Yield();
        Instance = this;

        var controllers = new IController[]
        {
            ControllersContainer.CreateController<MessageBus>()
        };

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
        Instance = null;
    }
}