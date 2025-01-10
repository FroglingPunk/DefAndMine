using Cysharp.Threading.Tasks;

public interface IBattleActor
{
    int Priority { get; }
    UniTask ExecuteTurnAsync(EBattleStage stage);
    bool IsSupportStage(EBattleStage stage);
}