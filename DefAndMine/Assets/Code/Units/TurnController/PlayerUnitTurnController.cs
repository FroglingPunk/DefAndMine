using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class PlayerUnitTurnController : IUnitTurnController
{
    public async UniTask ExecuteAsync(Unit unit, EBattleStage stage)
    {
        var unitActionsPanel = Object.FindObjectOfType<UnitActionsPanel>(true);
        unitActionsPanel.Show(unit);

        var action = (UnitAction)null;
        var cslTokenSource = (CancellationTokenSource)null;
        var actionContext = (UnitActionContext)null;

        unitActionsPanel.OnActionSelect += OnActionSelect;

        void OnActionSelect(UnitAction selectedAction)
        {
            if (selectedAction == null)
            {
                cslTokenSource?.Cancel();
            }
            else
            {
                action = selectedAction;
            }
        }

        while (actionContext == null)
        {
            action = null;

            while (action == null)
            {
                await UniTask.Yield();
            }

            cslTokenSource = new CancellationTokenSource();
            actionContext = await action.CreateContextAsync(unit, cslTokenSource.Token);
        }
        
        unitActionsPanel.Hide();

        await action.ExecuteAsync(actionContext);
    }
}