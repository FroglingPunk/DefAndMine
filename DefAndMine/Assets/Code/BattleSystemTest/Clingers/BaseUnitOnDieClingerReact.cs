using UnityEngine;

public abstract class BaseUnitOnDieClingerReact : MonoBehaviour, IUnitClinger
{
    public void Init(Battler unit)
    {
        unit.OnDie += React;
    }

    protected abstract void React(EDamageType damageType, Battler killer);
}