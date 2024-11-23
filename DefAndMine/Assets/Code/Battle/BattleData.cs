using UnityEngine;

[CreateAssetMenu(fileName = "BattleData", menuName = "Patterns/Battle Data", order = 54)]
public class BattleData : ScriptableObject
{
    [SerializeField] private UnitWaveSpawnData[] unitWaveSpawnData;

    public UnitWaveSpawnData[] UnitWaveSpawnData => unitWaveSpawnData;
}