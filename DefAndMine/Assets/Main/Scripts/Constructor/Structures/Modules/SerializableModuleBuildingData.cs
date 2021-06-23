using UnityEngine;

namespace Constructor.Structures
{
    [System.Serializable]
    public class SerializableModuleBuildingData : IModuleBuildingData
    {
        [SerializeField] private int prefabID;
        [SerializeField] private int modulePlaceID;

        public Module Prefab => PrefabsArchive.Instance.GetStructureModuleData(prefabID).Prefab;
        public int ModulePlaceID => modulePlaceID;


        public SerializableModuleBuildingData(int prefabID, int modulePlaceID)
        {
            this.prefabID = prefabID;
            this.modulePlaceID = modulePlaceID;
        }
    }
}