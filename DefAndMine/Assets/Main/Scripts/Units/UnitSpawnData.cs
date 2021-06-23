using UnityEngine;

[CreateAssetMenu(fileName = "UnitSpawnData", menuName = "Patterns/Units/Unit Spawn Data", order = 52)]
public class UnitSpawnData : ScriptableObject
{
    [SerializeField] private Unit unitPrefab;             // префаб
    [SerializeField] private int amount;                  // количество
    [SerializeField] private float delayBeforeStart;      // пауза между началом волны и первым спавном
    [SerializeField] private float delayBetweenSpawn;     // пауза между спавном юнитов


    public Unit UnitPrefab => unitPrefab;
    public int Amount => amount;
    public float DelayBeforeStart => delayBeforeStart;
    public float DelayBetweenSpawn => delayBetweenSpawn;
}