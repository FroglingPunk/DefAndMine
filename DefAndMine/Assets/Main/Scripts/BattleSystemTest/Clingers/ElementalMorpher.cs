using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalMorpher : BaseUnitGetDamageClingerReact
{
    private Dictionary<EElementalType, ElementPower> elementsPower = new Dictionary<EElementalType, ElementPower>();


    protected override void React(int damageSize, EDamageType damageType, Battler attacker)
    {

    }
}

public class ElementPower
{
    public float power;
}