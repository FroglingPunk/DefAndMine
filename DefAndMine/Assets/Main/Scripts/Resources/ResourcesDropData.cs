using UnityEngine;

[CreateAssetMenu(fileName = "ResourcesDropData", menuName = "Patterns/Resources Drop Data", order = 55)]
public class ResourcesDropData : ScriptableObject
{
    [SerializeField] private EResourcesDropType resourcesDropType;
    [SerializeField, Header("Если выбран EResourcesDropType.TimeByTime способ")] private float delayBetweenDrops;
    [SerializeField] private SerializableResourcesPackage drop;
    [SerializeField] private SerializableResourcesPackage startResources;


    public EResourcesDropType EResourcesDropType => resourcesDropType;
    public float DelayBetweenDrops => delayBetweenDrops;
    public SerializableResourcesPackage Drop => drop;
    public SerializableResourcesPackage StartResources => startResources;
}