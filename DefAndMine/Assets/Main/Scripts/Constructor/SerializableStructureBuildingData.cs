using UnityEngine;

namespace Contstructor
{
    [System.Serializable]
    public class SerializableStructureBuildingData : IStructureBuildingData
    {
        [SerializeField] private SerializableBlockBuildingData[] blocksBuildingData;
        [SerializeField] private EPlace place;
        [SerializeField] private string name;


        public SerializableStructureBuildingData(SerializableBlockBuildingData[] blocksBuildingData, EPlace place, string name)
        {
            this.blocksBuildingData = blocksBuildingData;
            this.place = place;
            this.name = name;
        }


        public Vector2Int[] Occupied
        {
            get
            {
                Vector2Int[] occupied = new Vector2Int[blocksBuildingData.Length];

                for (int i = 0; i < blocksBuildingData.Length; i++)
                {
                    occupied[i] = blocksBuildingData[i].CellID;
                }

                return occupied;
            }
        }
        public EPlace Place => place;
        public string Name => name;


        public Structure CreateInstance()
        {
            Structure instance = new GameObject("Structure (" + name + ")", typeof(Structure)).GetComponent<Structure>();

            for (int i = 0; i < blocksBuildingData.Length; i++)
            {
                SerializableBlockBuildingData blockBuildingData = blocksBuildingData[i];

                Block block = Object.Instantiate(blockBuildingData.Prefab, instance.transform);
                block.transform.localPosition =
                    new Vector3(blockBuildingData.CellID.x, 0, blockBuildingData.CellID.y);

                for (int p = 0; p < blockBuildingData.ModulesBuildingData.Length; p++)
                {
                    SerializableModuleBuildingData moduleBuildingData = blockBuildingData.ModulesBuildingData[p];
                    Module module = Object.Instantiate(moduleBuildingData.Prefab);
                    block.SetModule(module, moduleBuildingData.ModulePlaceID);
                }
            }

            return instance;
        }
    }
}