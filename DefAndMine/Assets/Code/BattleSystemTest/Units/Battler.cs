using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public abstract class Battler : MonoBehaviour
{
    public CompositeInt HPMax { get; private set; }
    public DamageResistance DamageResistance { get; private set; }
    public int HPCurrent { get; private set; }


    public event Action<int, EDamageType, Battler> OnGetDamage;
    public event Action<int, Battler> OnHeal;
    public event Action<EDamageType, Battler> OnDie;


    void Init()
    {
        HPMax = new CompositeInt();
        DamageResistance = new DamageResistance();

        List<IUnitClinger> clingers = new List<IUnitClinger>();
        GetComponentsInChildren(true, clingers);
        clingers.AddRange(GetComponents<IUnitClinger>());

        for (int i = 0; i < clingers.Count; i++)
        {
            clingers[i].Init(this);
        }

        HPCurrent = HPMax.Value;
    }


    public virtual void GetDamage(int damageSize, EDamageType damageType, Battler attacker)
    {
        int realDamage = (int)((1f - DamageResistance[damageType]) * damageSize);
        realDamage = Mathf.Clamp(realDamage, 0, int.MaxValue);

        HPCurrent = Mathf.Clamp(HPCurrent - realDamage, 0, HPMax.Value);

        OnGetDamage?.Invoke(damageSize, damageType, attacker);

        if (HPCurrent == 0)
        {
            Die(damageType, attacker);
        }
    }

    public virtual void Heal(int healSize, Battler healer)
    {
        HPCurrent = Mathf.Clamp(HPCurrent + healSize, 0, HPMax.Value);

        OnHeal?.Invoke(healSize, healer);
    }

    public virtual void Die(EDamageType damageType, Battler killer)
    {
        HPCurrent = 0;

        OnDie?.Invoke(damageType, killer);

        Destroy(gameObject);
    }
}