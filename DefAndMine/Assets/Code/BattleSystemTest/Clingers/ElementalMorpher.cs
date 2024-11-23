using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalMorpher : BaseUnitGetDamageClingerReact
{
    private Dictionary<EElementalType, ElementPower> elementsPower = new Dictionary<EElementalType, ElementPower>();


    protected override void React(int damageSize, EDamageType damageType, Battler attacker)
    {
        // random poka chto
        EElementalType elementalType = (EElementalType)Random.Range(0, 5);

        if (!elementsPower.ContainsKey(elementalType))
        {
            elementsPower.Add(elementalType, new ElementPower { power = damageSize });
        }
        else
        {
            elementsPower[elementalType].power += damageSize;
        }

        foreach (EElementalType element in elementsPower.Keys)
        {
            if (elementsPower[element].power >= Unit.HPMax.Value)
            {
                (Unit as Elemental).Morph(element);
                break;
            }
        }
    }
}

public class ElementPower
{
    public float power;
}