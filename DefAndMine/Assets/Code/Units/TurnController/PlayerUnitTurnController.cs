using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UniRx;

public class PlayerUnitTurnController : IUnitTurnController
{
    public async UniTask ExecuteAsync()
    {
        var units = BattleTurnSystem.Instance.UnitsList.GetUnitsByTeam(ETeam.Player);
        var unitsWithoutMoveAction = new List<Unit>();

        if (units == null || units.Count == 0)
        {
            return;
        }

        var unitActionsPanel = Object.FindObjectOfType<UnitActionsPanel>(true);

        while (units.Count > 0)
        {
            units.ForEach(u => u.Cell.Value.SetHighlightState(EHighlightState.PossibleTarget));
            var raycastPointer = new RaycastPointer<CellView>();
            var selectedUnit = (Unit)null;

            raycastPointer.OnClick.Subscribe(cellView =>
            {
                if (units.Contains(cellView.Cell.Unit))
                {
                    selectedUnit = cellView.Cell.Unit;
                }
            });

            while (selectedUnit == null)
            {
                await UniTask.Yield();
            }

            Field.Instance.SetCellsHighlightState(EHighlightState.None);
            raycastPointer.Dispose();
            unitActionsPanel.Show(selectedUnit);

            var action = (UnitAction)null;
            var cslTokenSource = (CancellationTokenSource)null;
            var actionContext = (UnitActionContext)null;
            var unselectUnit = false;

            unitActionsPanel.OnActionSelect += OnActionSelect;

            void OnActionSelect(UnitAction selectedAction)
            {
                if (selectedAction == null)
                {
                    cslTokenSource?.Cancel();
                }
                else
                {
                    cslTokenSource?.Cancel();
                    action = selectedAction;
                }
            }

            while (actionContext == null)
            {
                if (!unitsWithoutMoveAction.Contains(selectedUnit))
                {
                    action = selectedUnit.Context.BaseMoveAction;
                    cslTokenSource = new CancellationTokenSource();
                    actionContext = await action.ManualCreateContextAsync(selectedUnit, cslTokenSource.Token);

                    if (cslTokenSource.IsCancellationRequested)
                    {
                        await UniTask.Yield();
                        cslTokenSource = new CancellationTokenSource();
                        actionContext = await action.ManualCreateContextAsync(selectedUnit, cslTokenSource.Token);
                    }
                    else
                    {
                        unitActionsPanel.Hide();
                        await action.ExecuteAsync(actionContext);
                        unitsWithoutMoveAction.Add(selectedUnit);
                        unitActionsPanel.Show(selectedUnit);
                        actionContext = null;
                    }
                }
                else
                {
                    action = null;

                    while (action == null)
                    {
                        await UniTask.Yield();

                        if (Input.GetMouseButtonDown(1))
                        {
                            unselectUnit = true;
                            break;
                        }
                    }

                    if (unselectUnit)
                    {
                        break;
                    }

                    cslTokenSource = new CancellationTokenSource();
                    actionContext = await action.ManualCreateContextAsync(selectedUnit, cslTokenSource.Token);
                }
            }

            if (unselectUnit)
            {
                unitActionsPanel.Hide();
                continue;
            }

            unitActionsPanel.Hide();
            await action.ExecuteAsync(actionContext);
            units.Remove(selectedUnit);
        }
    }
}