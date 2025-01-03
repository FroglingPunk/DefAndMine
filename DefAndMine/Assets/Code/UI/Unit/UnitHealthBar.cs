using UniRx;
using UnityEngine;

public class UnitHealthBar : MonoBehaviour
{
    [SerializeField] private Transform _rectCurrentHp;


    public void Init(Unit unit)
    {
        unit.HealthCurrent.Subscribe(_ =>
        {
            _rectCurrentHp.localScale = new Vector3((float)unit.HealthCurrent.Value / unit.HealthMax.Value, 1f, 1f);
        });
    }
}