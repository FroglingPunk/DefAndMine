using UnityEngine;

namespace Constructor.Structures
{
    [CreateAssetMenu(fileName = "ModuleBuildingData", menuName = "Patterns/Constructor/Structures/Module Building Data", order = 51)]
    public class ModuleBuildingData : ScriptableObject, IModuleBuildingData
    {
        [SerializeField] private Module prefab;


        public Module Prefab => prefab;
    }
}