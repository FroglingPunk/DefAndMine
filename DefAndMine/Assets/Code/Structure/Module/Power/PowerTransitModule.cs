using UnityEngine;

public class PowerTransitModule : Module
{
    [field: SerializeField] public EPowerTransitType TransitType { get; private set; }
}

public enum EPowerTransitType
{
    Input,
    Output
}