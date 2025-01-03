using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Actions/Move", fileName = "MoveAction", order = 0)]
public class MoveAction : UnitAction
{
    public override async UniTask<UnitActionContext> CreateContextAsync(Unit unit, CancellationToken cancellationToken)
    {
        var possibleForMoveCells = Field.Instance.SetEchoHighlight(unit.Cell, unit.MovementDistance, true);

        var raycastPointer = new RaycastPointer<Cell>();
        var selectedCell = (Cell)null;

        raycastPointer.OnClick.Subscribe(cell =>
        {
            if (possibleForMoveCells.Contains(cell))
            {
                selectedCell = cell;
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
                Field.Instance.SetCellsHighlightState(false);
                return null;
            }
        }

        raycastPointer.Dispose();
        Field.Instance.SetCellsHighlightState(false);

        return new UnitActionContext
        {
            sourceUnit = unit,
            sourceCell = unit.Cell,
            targetCell = selectedCell
        };
    }

    public override async UniTask ExecuteAsync(UnitActionContext context)
    {
        if (!context.sourceCell.TryBuildPath(context.targetCell, out var path))
        {
            Debug.LogError("Не удалось построить путь для передвижения юнита");
            return;    
        }
        
        await context.sourceUnit.MoveAsync(path);
    }
}