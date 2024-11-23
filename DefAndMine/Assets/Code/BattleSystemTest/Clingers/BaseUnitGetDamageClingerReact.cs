using UnityEngine;

public abstract class BaseUnitGetDamageClingerReact : MonoBehaviour, IUnitClinger
{
    protected Battler Unit { get; private set; }


    public void Init(Battler unit)
    {
        Unit = unit;
        unit.OnGetDamage += React;
    }

    protected abstract void React(int damageSize, EDamageType damageType, Battler attacker);
}