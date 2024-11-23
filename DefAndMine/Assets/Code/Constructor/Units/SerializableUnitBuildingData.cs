using UnityEngine;

namespace Constructor.Units
{
    [System.Serializable]
    public class SerializableUnitBuildingData : IUnitBuildingData
    {
        [SerializeField] private int[] modulesBuildingDataPrefabsID;
        [SerializeField] private string name;


        public SerializableUnitBuildingData(int[] modulesBuildingDataPrefabsID, string name)
        {
            this.modulesBuildingDataPrefabsID = modulesBuildingDataPrefabsID;
            this.name = name;
        }


        public string Name => name;
        public ResourcesStorage Cost
        {
            get
            {
                ResourcesStorage cost = new ResourcesStorage();
                for (int i = 0; i < modulesBuildingDataPrefabsID.Length; i++)
                {
                    cost.Increase(PrefabsArchive.Instance.GetUnitModuleData(modulesBuildingDataPrefabsID[i]).Cost.All);
                }

                return cost;
            }
        }


        public Unit CreateInstance()
        {
            UnitBody unitBodyPrefab = null;
            for (int i = 0; i < modulesBuildingDataPrefabsID.Length; i++)
            {
                ModuleBuildingData moduleBuildingData = PrefabsArchive.Instance.GetUnitModuleData(modulesBuildingDataPrefabsID[i]);
                if (moduleBuildingData.Prefab is UnitBody)
                {
                    unitBodyPrefab = moduleBuildingData.Prefab as UnitBody;
                    break;
                }
            }

            if (unitBodyPrefab == null)
            {
                Debug.LogError("UnitBody not found in UnitBuildingData modules");
                return null;
            }

            UnitBody unitBody = Object.Instantiate(unitBodyPrefab);
            Unit instance = unitBody.gameObject.AddComponent<Unit>();
            instance.name = ("Unit (" + Name + ")");

            for (int i = 0; i < modulesBuildingDataPrefabsID.Length; i++)
            {
                ModuleBuildingData moduleBuildingData = PrefabsArchive.Instance.GetUnitModuleData(modulesBuildingDataPrefabsID[i]);

                if (moduleBuildingData.Prefab != unitBodyPrefab)
                {
                    Module module = Object.Instantiate(moduleBuildingData.Prefab, instance.transform);
                    unitBody.SetModule(module);
                }
            }

            return instance;
        }
    }
}