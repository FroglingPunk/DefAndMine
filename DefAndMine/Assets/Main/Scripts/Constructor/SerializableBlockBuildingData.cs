using UnityEngine;

namespace Contstructor
{
    [System.Serializable]
    public class SerializableBlockBuildingData : IBlockBuildingData
    {
        [SerializeField] private int prefabID;
        [SerializeField] private Vector2Int cellID;
        [SerializeField] private SerializableModuleBuildingData[] modulesBuildingData;


        public Block Prefab => PrefabsArchive.Instance.GetBlock(prefabID);
        public Vector2Int CellID => cellID;
        public SerializableModuleBuildingData[] ModulesBuildingData => modulesBuildingData;


        public SerializableBlockBuildingData(int prefabID, Vector2Int cellID, SerializableModuleBuildingData[] modulesBuildingData)
        {
            this.prefabID = prefabID;
            this.cellID = cellID;
            this.modulesBuildingData = modulesBuildingData;
        }
    }
}