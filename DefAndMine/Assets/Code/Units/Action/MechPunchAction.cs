using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Actions/MechPunch", fileName = "MechPunchAction", order = 0)]
public class MechPunchAction : UnitAction
{
    public override async UniTask<UnitActionContext> ManualCreateContextAsync(Unit unit,
        CancellationToken cancellationToken)
    {
        var possibleCells = new List<Cell>();

        for (var dir = EDirection.N; dir < EDirection.NW; dir += 2)
        {
            var neighbour = unit.Cell.Value.Neighbour(dir);

            if (neighbour != null && neighbour.Unit != null)
            {
                possibleCells.Add(neighbour);
                neighbour.SetHighlightState(EHighlightState.PossibleTarget);
            }
        }

        var raycastPointer = new RaycastPointer<CellView>();
        var selectedCell = (Cell)null;

        raycastPointer.OnClick.Subscribe(cellView =>
        {
            if (possibleCells.Contains(cellView.Cell))
            {
                selectedCell = cellView.Cell;
            }
        });

        while (selectedCell == null)
        {
            try
            {
                await UniTask.Yield(cancellationToken);
            }
            catch
            {
                raycastPointer.Dispose();
                Field.Instance.SetCellsHighlightState(EHighlightState.None);
                return null;
            }
        }

        raycastPointer.Dispose();
        Field.Instance.SetCellsHighlightState(EHighlightState.None);

        return new UnitActionContext
        {
            sourceUnit = unit,
            direction = unit.Cell.Value.Direction(selectedCell)
        };
    }

    public override async UniTask<UnitActionContext> AICreateContextAsync(Unit unit)
    {
        for (var dir = EDirection.N; dir < EDirection.NW; dir += 2)
        {
            var neighbour = unit.Cell.Value.Neighbour(dir);

            if (neighbour != null && neighbour.Unit != null && neighbour.Unit != unit)
            {
                return new UnitActionContext
                {
                    sourceUnit = unit,
                    direction = dir
                };
            }
        }

        return null;
    }

    public override async UniTask ExecuteAsync(UnitActionContext context)
    {
        var targetCell = context.sourceUnit.Cell.Value.Neighbour(context.direction);

        if (targetCell != null && targetCell.Unit != null)
        {
            await targetCell.Unit.PushAsync(context.direction);
        }
    }

    public override void SetupMarksForDeferredAction(DeferredAction deferredAction, ref CompositeDisposable disposables,
        ref List<GameObject> marksGo)
    {
        var markSource = Instantiate(CellSpritesStorage.Instance.MarkSourceTemplate,
            deferredAction.context.sourceUnit.Cell.Value.Transform);
        markSource.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        var directionCell = deferredAction.context.sourceUnit.Cell.Value.Neighbour(deferredAction.context.direction);

        var markTarget = Instantiate(CellSpritesStorage.Instance.MarkTargetTemplate, directionCell.Transform);
        markTarget.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        marksGo = new()
        {
            markSource,
            markTarget
        };

        disposables.Add(deferredAction.context.sourceUnit.Cell.Subscribe(c =>
        {
            // если умер - отмена
            if (!deferredAction.context.sourceUnit.IsAlive)
            {
                BattleTurnSystem.Instance.DeferredActionsList.RemoveAction(deferredAction);
                return;
            }

            var targetCell = deferredAction.context.sourceUnit.Cell.Value.Neighbour(deferredAction.context.direction);

            // если юнит упёрся в край карты и целевая ячейка теперь за краем карты, т.е. недоступна - отмена
            if (targetCell == null)
            {
                BattleTurnSystem.Instance.DeferredActionsList.RemoveAction(deferredAction);
                return;
            }

            markSource.transform.SetParent(deferredAction.context.sourceUnit.Cell.Value.Transform);
            markSource.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

            markTarget.transform.SetParent(targetCell.Transform);
            markTarget.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        }));
    }
}