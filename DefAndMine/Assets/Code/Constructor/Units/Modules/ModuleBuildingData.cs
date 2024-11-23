using UnityEngine;

namespace Constructor.Units
{
    [CreateAssetMenu(fileName = "ModuleBuildingData", menuName = "Patterns/Constructor/Units/Module Building Data", order = 51)]
    public class ModuleBuildingData : ScriptableObject, IModuleBuildingData
    {
        [SerializeField] private Module prefab;
        [SerializeField] private Sprite icon;
        [SerializeField] private string unicalName;
        [SerializeField] private string description;
        [SerializeField] private SerializableResourcesPackage cost;


        public Module Prefab => prefab;
        public Sprite Icon => icon;
        public string Name => unicalName;
        public string Decsription => description;
        public SerializableResourcesPackage Cost => cost;
    }
}