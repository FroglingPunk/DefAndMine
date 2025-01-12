using System.Collections.Generic;
using UnityEngine;

public class UnitContext : MonoBehaviour
{
    [field: SerializeField] public List<UnitAction> Actions { get; private set; }
    [field: SerializeField] public UnitAction BaseMoveAction { get; private set; }
    [field: SerializeField] public EWeightCategory WeightCategory { get; private set; }
}