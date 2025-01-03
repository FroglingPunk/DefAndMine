using Cysharp.Threading.Tasks;

public interface IUnitTurnController
{
    UniTask ExecuteAsync(Unit unit);
}