using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Actions/MechPunch", fileName = "MechPunchAction", order = 0)]
public class MechPunchAction : UnitAction
{
    public override async UniTask<UnitActionContext> CreateContextAsync(Unit unit, CancellationToken cancellationToken)
    {
        var possibleCells = new List<Cell>();

        for (var dir = EDirection.N; dir < EDirection.NW; dir += 2)
        {
            var neighbour = unit.Cell.Neighbour(dir);

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
            sourceCell = unit.Cell,
            targetCell = selectedCell,
            targetUnit = selectedCell.Unit
        };
    }

    public override async UniTask ExecuteAsync(UnitActionContext context)
    {
        var direction = context.sourceCell.Direction(context.targetCell);
        await context.targetUnit.PushAsync(direction);
    }
}