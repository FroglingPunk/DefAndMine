using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(UnitContext))]
public class Unit : MonoBehaviour, IBattleActor
{
    [SerializeField] private UnitHealthBar _healthBar;

    public int Priority => 0;
    public ETeam Team { get; private set; }
    public Cell Cell { get; private set; }
    public int MovementDistance { get; private set; }
    public byte MovePriority { get; private set; }
    public UnitContext Context { get; private set; }
    public IReadOnlyReactiveProperty<int> HealthCurrent => _healthCurrent;
    public IReadOnlyReactiveProperty<int> HealthMax => _healthMax;

    private readonly ReactiveProperty<int> _healthCurrent = new();
    private readonly ReactiveProperty<int> _healthMax = new();




    public async UniTask ExecuteTurnAsync(EBattleStage stage)
    {
        switch (stage)
        {
            case EBattleStage.PreparingDeferred:
                await new AIUnitTurnController().ExecuteAsync(this, stage);
                break;

            case EBattleStage.ExecutionDeferred:
                await new AIUnitTurnController().ExecuteAsync(this, stage);
                break;

            case EBattleStage.PlayerTurn:
                await new PlayerUnitTurnController().ExecuteAsync(this, stage);
                break;
        }
    }

    public bool IsSupportStage(EBattleStage stage)
    {
        return stage switch
        {
            EBattleStage.PreparingDeferred => Team == ETeam.Enemy,
            EBattleStage.ExecutionDeferred => Team == ETeam.Enemy,
            EBattleStage.PlayerTurn => Team == ETeam.Player,
            _ => false
        };
    }


    public void Init(ETeam team, Cell cell)
    {
        Context = GetComponent<UnitContext>();

        Team = team;
        Cell = cell;
        cell.Unit = this;
        MovementDistance = Random.Range(2, 5);
        MovePriority = (byte)(Random.Range(int.MinValue, int.MaxValue) % byte.MaxValue);

        _healthMax.Value = Random.Range(2, 6);
        _healthCurrent.Value = _healthMax.Value;

        transform.position = cell.Transform.position;

        _healthBar.Init(this);
    }

    public async UniTask MoveAsync(IEnumerable<Cell> path)
    {
        foreach (var cell in path)
        {
            transform.position = cell.Transform.position;

            Cell.Unit = null;
            Cell = cell;
            cell.Unit = this;

            await UniTask.WaitForSeconds(0.2f);
        }
    }

    public async UniTask PushAsync(EDirection direction)
    {
        var nextCell = Cell.Neighbour(direction);

        if (nextCell != null)
        {
            await MoveAsync(new Cell[] { nextCell });
        }
        else
        {
            var defPos = transform.position;
            var endPos = Cell.Transform.position +
                         (Cell.Transform.position - Cell.Neighbour(direction.Opposite()).Transform.position) * 0.5f;

            for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime * 2)
            {
                transform.position = Vector3.Lerp(defPos, endPos, lerp);
                await UniTask.Yield();
            }

            transform.position = endPos;
            await UniTask.Yield();
            
            for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime * 2)
            {
                transform.position = Vector3.Lerp(endPos, defPos, lerp);
                await UniTask.Yield();
            }

            transform.position = defPos;
            await UniTask.Yield();
        }
    }
}