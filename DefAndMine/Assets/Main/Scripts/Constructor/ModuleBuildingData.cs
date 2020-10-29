using UnityEngine;

namespace Contstructor
{
    [CreateAssetMenu(fileName = "ModuleBuildingData", menuName = "Patterns/Constructor/Module Building Data", order = 51)]
    public class ModuleBuildingData : ScriptableObject, IModuleBuildingData
    {
        [SerializeField] private Module prefab;


        public Module Prefab => prefab;
    }
}