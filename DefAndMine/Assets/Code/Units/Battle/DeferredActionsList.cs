using System.Collections.Generic;
using UniRx;

public class DeferredActionsList
{
    public IReadOnlyReactiveCollection<DeferredAction> Actions => _actions;
 
    private readonly ReactiveCollection<DeferredAction> _actions = new();


    public void AddAction(DeferredAction action)
    {
        _actions.Add(action);
    }

    public void RemoveAction(DeferredAction action)
    {
        _actions.Remove(action);
    }

    public List<DeferredAction> GetActionsByStage(EBattleStage stage)
    {
        var stageActions = new List<DeferredAction>();

        for (var i = _actions.Count - 1; i >= 0; i--)
        {
            if (_actions[i].stage == stage)
            {
                stageActions.Add(_actions[i]);
                _actions.RemoveAt(i);
            }
        }

        return stageActions;
    }
}