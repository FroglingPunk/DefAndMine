//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ModularUnitBehaviour : BaseUnitBehaviour
//{
//    public string Team { get; protected set; }
//    public EUnitBehaviour Behaviour { get; protected set; }
//    public UnitHealth Health { get; protected set; }


//    public abstract MovementPath MovementPath { get; protected set; }
//    public abstract Structure TargetStructure { get; protected set; }
//    public abstract Unit TargetUnit { get; protected set; }
//    public abstract ResourcesStorage Drop { get; }


//    public abstract void Init(string team, MovementPath movementPath, Unit targetUnit, Structure targetStructure);


//    public virtual void GetDamage(int damage)
//    {
//        if (Health.Value > damage)
//        {
//            Health.Value -= damage;
//        }
//        else
//        {
//            Die();
//        }
//    }

//    public virtual void Die()
//    {
//        Health.Value = 0;

//        Destroy(gameObject);
//    }

//}