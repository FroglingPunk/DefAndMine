using System.Collections.Generic;
using UnityEngine;

public class UnitContext : MonoBehaviour
{
    [field: SerializeField] public List<UnitAction> Actions { get; private set; }
}