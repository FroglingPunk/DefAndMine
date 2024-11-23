using System.Collections.Generic;

public class DamageResistance
{
    private Dictionary<EDamageType, CompositeFloat> resistances = new Dictionary<EDamageType, CompositeFloat>();


    public float this[EDamageType damageType]
    {
        get
        {
            return resistances.ContainsKey(damageType) ? resistances[damageType].Value : 0f;
        }
    }


    public void AddComponent(EDamageType damageType, float value)
    {
        if (!resistances.ContainsKey(damageType))
        {
            resistances.Add(damageType, new CompositeFloat());
        }

        resistances[damageType].AddComponent(value);
    }

    public void RemoveComponent(EDamageType damageType, float value)
    {
        if (resistances.ContainsKey(damageType))
        {
            resistances[damageType].RemoveComponent(value);
        }
    }

    public void AddBuff(EDamageType damageType, float value)
    {
        if (!resistances.ContainsKey(damageType))
        {
            resistances.Add(damageType, new CompositeFloat());
        }

        resistances[damageType].AddBuff(value);
    }

    public void RemoveBuff(EDamageType damageType, float value)
    {
        if (resistances.ContainsKey(damageType))
        {
            resistances[damageType].RemoveBuff(value);
        }
    }
}