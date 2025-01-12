using Cysharp.Threading.Tasks;

public class DeferredAction
{
    public readonly UnitAction action;
    public readonly UnitActionContext context;
    public readonly EBattleStage stage;


    public DeferredAction(UnitAction action, UnitActionContext context, EBattleStage stage)
    {
        this.action = action;
        this.context = context;
        this.stage = stage;
    }

    public async UniTask ExecuteAsync()
    {
        await action.ExecuteAsync(context);
    }
}