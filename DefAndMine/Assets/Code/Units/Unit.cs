using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[RequireComponent(typeof(UnitContext))]
public class Unit : MonoBehaviour
{
    [SerializeField] private UnitHealthBar _healthBar;

    public ETeam Team { get; private set; }
    public int MovementDistance { get; private set; }
    public byte MovePriority { get; private set; }
    public UnitContext Context { get; private set; }
    
    public IReadOnlyReactiveProperty<Cell> Cell => _cell;
    public IReadOnlyReactiveProperty<int> HealthCurrent => _healthCurrent;
    public IReadOnlyReactiveProperty<int> HealthMax => _healthMax;

    public bool IsAlive => _healthCurrent.Value > 0;
    public bool CanFly { get; private set; }
    public bool CanSwim => Context.WeightCategory != EWeightCategory.Lightweight;


    private readonly ReactiveProperty<Cell> _cell = new();
    private readonly ReactiveProperty<int> _healthCurrent = new();
    private readonly ReactiveProperty<int> _healthMax = new();


    public void Init(ETeam team, Cell cell)
    {
        Context = GetComponent<UnitContext>();

        Team = team;
        _cell.Value = cell;
        _cell.Value.Unit = this;
        MovementDistance = Random.Range(2, 5);
        MovePriority = (byte)(Random.Range(int.MinValue, int.MaxValue) % byte.MaxValue);

        _healthMax.Value = Random.Range(1, 3);
        _healthCurrent.Value = _healthMax.Value;

        transform.position = cell.Transform.position;

        _healthBar.Init(this);
    }

    public async UniTask MoveAsync(IEnumerable<Cell> path)
    {
        foreach (var cell in path)
        {
            transform.position = cell.Transform.position;

            _cell.Value.Unit = null;
            _cell.Value = cell;
            _cell.Value.Unit = this;

            await UniTask.WaitForSeconds(0.2f);
        }
    }

    public async UniTask PushAsync(EDirection direction)
    {
        var nextCell = _cell.Value.Neighbour(direction);

        if (nextCell != null && nextCell.Unit == null && (nextCell.Content == null || !nextCell.Content.IsSolid))
        {
            var defPos = transform.position;
            var endPos = nextCell.Transform.position;
            
            for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime * 2)
            {
                transform.position = Vector3.Lerp(defPos, endPos, lerp);
                await UniTask.Yield();
            }
            
            transform.position = endPos;
            _cell.Value.Unit = null;
            _cell.Value = nextCell;
            _cell.Value.Unit = this;
            
            if (nextCell.Type == ECellType.Water && !CanSwim)
            {
                Die();
                await UniTask.Yield();
                return;
            }
        }
        else
        {
            var defPos = transform.position;
            var endPos = _cell.Value.Transform.position +
                         (_cell.Value.Transform.position - _cell.Value.Neighbour(direction.Opposite()).Transform.position) * 0.5f;

            for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime * 2)
            {
                transform.position = Vector3.Lerp(defPos, endPos, lerp);
                await UniTask.Yield();
            }

            transform.position = endPos;

            if (nextCell != null)
            {
                GetDamage(1);

                if (!IsAlive)
                {
                    await UniTask.Yield();
                    return;
                }
            }

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

    public void GetDamage(int damage)
    {
        _healthCurrent.Value = Mathf.Clamp(_healthCurrent.Value - damage, 0, _healthMax.Value);

        if (_healthCurrent.Value == 0)
        {
            Die();
        }
    }

    public void Heal(int healing)
    {
        _healthCurrent.Value = Mathf.Clamp(_healthCurrent.Value + healing, 0, _healthMax.Value);
    }

    public void Die()
    {
        _cell.Value.Unit = null;
        _healthCurrent.Value = 0;
        gameObject.SetActive(false);
    }
}