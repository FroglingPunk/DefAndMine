using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(UnitContext))]
public class Unit : MonoBehaviour
{
    [SerializeField] private UnitHealthBar _healthBar;

    public ETeam Team { get; private set; }
    public Cell Cell { get; private set; }
    public int MovementDistance { get; private set; }
    public byte MovePriority { get; private set; }
    public UnitContext Context { get; private set; }
    public IReadOnlyReactiveProperty<int> HealthCurrent => _healthCurrent;
    public IReadOnlyReactiveProperty<int> HealthMax => _healthMax;

    private readonly ReactiveProperty<int> _healthCurrent = new();
    private readonly ReactiveProperty<int> _healthMax = new();


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

        transform.position = cell.transform.position;

        _healthBar.Init(this);
    }

    public async UniTask MoveAsync(IEnumerable<Cell> path)
    {
        foreach (var cell in path)
        {
            transform.position = cell.transform.position;

            Cell.Unit = null;
            Cell = cell;
            cell.Unit = this;

            await UniTask.WaitForSeconds(0.2f);
        }
    }
}