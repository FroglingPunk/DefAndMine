using Cysharp.Threading.Tasks;

public class LocalSceneContainer : SceneContainerBase
{
    public static LocalSceneContainer Instance { get; private set; }

    public SceneInitializerBase Initializer { get; protected set; }

    public static MessageBus MessageBus => Instance.ControllersContainer.GetController<MessageBus>();

    public static T GetController<T>() where T : class, IController
    {
        if (Instance.ControllersContainer.ContainsController<T>())
        {
            return Instance.ControllersContainer.GetController<T>();
        }
        else
        {
            return GlobalContainer.Instance.ControllersContainer.GetController<T>();
        }
    }


    public LocalSceneContainer(SceneInitializerBase initializer)
    {
        Initializer = initializer;
    }


    public override async UniTask InitAsync()
    {
        Instance = this;
        ControllersContainer.CreateController<MessageBus>();
        await Initializer.InitAsync();
    }

    public override async UniTask DeinitAsync()
    {
        await Initializer.DeinitAsync();
        Instance = null;
    }
}