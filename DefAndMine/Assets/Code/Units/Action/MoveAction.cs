using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Actions/Move", fileName = "MoveAction", order = 0)]
public class MoveAction : UnitAction
{
    public override async UniTask<UnitActionContext> ManualCreateContextAsync(Unit unit, CancellationToken cancellationToken)
    {
        var possibleForMoveCells = Field.Instance.SetEchoHighlight(unit, unit.MovementDistance, EHighlightState.PossibleTarget);

        var raycastPointer = new RaycastPointer<CellView>();
        var selectedCell = (Cell)null;

        raycastPointer.OnClick.Subscribe(cellView =>
        {
            if (possibleForMoveCells.Contains(cellView.Cell))
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
            sourceCell = unit.Cell.Value,
            targetCell = selectedCell
        };
    }

    public override async UniTask<UnitActionContext> AICreateContextAsync(Unit unit)
    {
        return default;
    }
    
    public override async UniTask ExecuteAsync(UnitActionContext context)
    {
        if (!context.sourceCell.TryBuildPath(context.targetCell, context.sourceUnit, out var path))
        {
            Debug.LogError("Не удалось построить путь для передвижения юнита");
            return;    
        }
        
        await context.sourceUnit.MoveAsync(path);
    }

    public override void SetupMarksForDeferredAction(DeferredAction deferredAction, ref CompositeDisposable disposables, ref List<GameObject> marksGo)
    {
        
    }
}