using Cysharp.Threading.Tasks;

public interface IController
{
    public UniTask ConstructAsync();
    public UniTask InitAsync();
    public UniTask DeinitAsync();
}