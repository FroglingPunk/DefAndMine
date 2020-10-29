using UnityEngine;

namespace Contstructor
{
    [System.Serializable]
    public class SerializableModuleBuildingData : IModuleBuildingData
    {
        [SerializeField] private int prefabID;
        [SerializeField] private int modulePlaceID;

        public Module Prefab => PrefabsArchive.Instance.GetModule(prefabID);
        public int ModulePlaceID => modulePlaceID;


        public SerializableModuleBuildingData(int prefabID, int modulePlaceID)
        {
            this.prefabID = prefabID;
            this.modulePlaceID = modulePlaceID;
        }
    }
}