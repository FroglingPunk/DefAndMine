using UnityEngine;

namespace Constructor.Units
{
    [CreateAssetMenu(fileName = "StructureBuildingData", menuName = "Patterns/Constructor/Units/Unit Building Data", order = 51)]
    [System.Serializable]
    public class UnitBuildingData : ScriptableObject, IUnitBuildingData
    {
        [SerializeField] private Unit prefab;
        [SerializeField] private SerializableResourcesPackage cost;


        public ResourcesStorage Cost => new ResourcesStorage(cost.All);


        public Unit CreateInstance()
        {
            return Instantiate(prefab);
        }
    }
}