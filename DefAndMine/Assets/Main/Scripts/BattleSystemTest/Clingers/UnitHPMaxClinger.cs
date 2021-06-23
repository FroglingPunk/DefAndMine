using UnityEngine;

public class UnitHPMaxClinger : MonoBehaviour, IUnitClinger
{
    [SerializeField] private int component;
    [SerializeField] private float buffMultiply;


    public void Init(Battler unit)
    {
        unit.HPMax.AddComponent(component);
        unit.HPMax.AddBuff(buffMultiply);
    }

    public void Deinit(Battler unit)
    {
        unit.HPMax.RemoveComponent(component);
        unit.HPMax.RemoveBuff(buffMultiply);
    }
}