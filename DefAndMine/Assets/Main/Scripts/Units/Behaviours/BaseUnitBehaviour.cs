using Constructor.Structures;
using UnityEngine;

public class UnitHealth : NotifyingVariable<int>
{
    public UnitHealth() : base() { }
    public UnitHealth(int startValue) : base(startValue) { }
}

public abstract class BaseUnitBehaviour : MonoBehaviour
{
    public ETeam Team { get; protected set; }
    public EUnitBehaviour Behaviour { get; protected set; }
    public UnitHealth Health { get; protected set; }


    public virtual MovementPath MovementPath { get; protected set; }
    public virtual Structure TargetStructure { get; protected set; }
    public virtual Unit TargetUnit { get; protected set; }
    public virtual ResourcesStorage Drop { get; }


    public abstract void Init(ETeam team, MovementPath movementPath, Structure targetStructure);


    public virtual void GetDamage(int damage)
    {
        if (Health.Value > damage)
        {
            Health.Value -= damage;
        }
        else
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Health.Value = 0;

        Destroy(gameObject);
    }
}