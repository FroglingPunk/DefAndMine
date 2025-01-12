using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Actions/RocketShoot", fileName = "RocketShootAction", order = 0)]
public class RocketShootAction : UnitAction
{
    [SerializeField] private GameObject _rocketTemplate;


    public override async UniTask<UnitActionContext> ManualCreateContextAsync(Unit unit,
        CancellationToken cancellationToken)
    {
        return default;
    }

    public override async UniTask<UnitActionContext> AICreateContextAsync(Unit unit)
    {
        var targetUnits = BattleTurnSystem.Instance.UnitsList.GetUnitsByTeam(unit.Team.Opposite());

        if (targetUnits == null || targetUnits.Count == 0)
        {
            return null;
        }

        var unitPosX = unit.Cell.Value.PosX;
        var unitPosZ = unit.Cell.Value.PosZ;

        for (var i = 0; i < targetUnits.Count; i++)
        {
            var target = targetUnits[i];
            var targetPosX = target.Cell.Value.PosX;
            var targetPosZ = target.Cell.Value.PosZ;

            if (targetPosX == unitPosX)
            {
                return new UnitActionContext
                {
                    sourceUnit = unit,
                    direction = targetPosZ > unitPosZ ? EDirection.N : EDirection.S
                };
            }

            if (targetPosZ == unitPosZ)
            {
                return new UnitActionContext
                {
                    sourceUnit = unit,
                    direction = targetPosX > unitPosX ? EDirection.E : EDirection.W
                };
            }

            if (Mathf.Abs(targetPosX - unitPosX) == Mathf.Abs(targetPosZ - unitPosZ))
            {
                return new UnitActionContext
                {
                    sourceUnit = unit,
                    direction = unit.Cell.Value.Direction(target.Cell.Value)
                };
            }
        }

        return null;
    }

    public override async UniTask ExecuteAsync(UnitActionContext context)
    {
        var startCell = context.sourceUnit.Cell.Value;
        var endCell = CalculateTarget(context);
        
        if (endCell == null)
        {
            await UniTask.WaitForSeconds(0.2f);
            return;
        }

        var rocket = Instantiate(_rocketTemplate);
        _rocketTemplate.transform.SetPositionAndRotation(context.sourceUnit.transform.position, Quaternion.identity);

        var flyTime = startCell.Distance(endCell) * 0.2f;

        for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime / flyTime)
        {
            rocket.transform.position = Vector3.Lerp(startCell.Transform.position, endCell.Transform.position, lerp);
            await UniTask.Yield();
        }

        rocket.transform.position = endCell.Transform.position;

        Destroy(rocket.gameObject);

        if (endCell.Unit != null && !endCell.Height.IsUpper(startCell.Height))
        {
            endCell.Unit.GetDamage(1);
        }

        await UniTask.WaitForSeconds(0.2f);
    }

    public override void SetupMarksForDeferredAction(DeferredAction deferredAction, ref CompositeDisposable disposables,
        ref List<GameObject> marksGo)
    {
        var markSource = Instantiate(CellSpritesStorage.Instance.MarkSourceTemplate,
            deferredAction.context.sourceUnit.Cell.Value.Transform);
        markSource.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        var endCell = CalculateTarget(deferredAction.context);

        var markTarget = Instantiate(CellSpritesStorage.Instance.MarkTargetTemplate, endCell.Transform);
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

            var targetCell = CalculateTarget(deferredAction.context);
            
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


    private Cell CalculateTarget(UnitActionContext context)
    {
        var startCell = context.sourceUnit.Cell.Value;
        var deltaField = context.direction.ToDeltaField();

        for (var i = 1; i < Field.Width; i++)
        {
            var cell = Field.Instance[startCell.PosX + deltaField.x * i, startCell.PosZ + deltaField.y * i];

            if (cell == null)
            {
                return null;
            }

            if (cell.Height.IsUpper(startCell.Height))
            {
                // если целевая ячейка выше стартовой, но она соседняя, то отменяем выстрел
                // мб добавить вариант разрешать выстрел и наносить урон самому себе
                // return i == 1 ? null : cell;

                return cell;
            }

            if (cell.Unit != null)
            {
                return cell;
            }

            if (cell.Content != null && cell.Content.IsSolid)
            {
                return cell;
            }

            if (Field.Instance[startCell.PosX + deltaField.x * (i + 1), startCell.PosZ + deltaField.y * (i + 1)] ==
                null)
            {
                return cell;
            }
        }

        return null;
    }
}