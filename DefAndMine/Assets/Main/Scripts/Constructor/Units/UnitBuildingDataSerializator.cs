using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Constructor.Units
{
    public static class UnitBuildingDataSerializator
    {
        public static SerializableUnitBuildingData Serialize(List<ModuleBuildingData> modulesBuildingData)
        {
            int[] modulesBuildingDataPrefabsID = new int[modulesBuildingData.Count];

            for (int i = 0; i < modulesBuildingDataPrefabsID.Length; i++)
            {
                modulesBuildingDataPrefabsID[i] = PrefabsArchive.Instance.GetIndex(modulesBuildingData[i]);
            }

            return new SerializableUnitBuildingData(modulesBuildingDataPrefabsID, "UnitDraft");
        }

        public static void Write(SerializableUnitBuildingData unitBuildingData)
        {
            string json = JsonUtility.ToJson(unitBuildingData);
            File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "unitBuildingData.txt"), json);
        }

        public static SerializableUnitBuildingData Read()
        {
            string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "unitBuildingData.txt"));
            SerializableUnitBuildingData unitBuildingData = JsonUtility.FromJson<SerializableUnitBuildingData>(json);

            return unitBuildingData;
        }
    }
}