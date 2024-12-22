// using UnityEngine;
//
// namespace Constructor
// {
//     public class PrefabsArchive : MonoBehaviour
//     {
//         private static PrefabsArchive instance;
//         public static PrefabsArchive Instance
//         {
//             get
//             {
//                 if (instance == null)
//                 {
//                     instance = FindObjectOfType<PrefabsArchive>();
//
//                     if (instance == null)
//                     {
//                         instance = Instantiate(Resources.Load<PrefabsArchive>("PrefabsArchive"));
//                     }
//
//                     DontDestroyOnLoad(instance.gameObject);
//                 }
//
//                 return instance;
//             }
//         }
//
//
//         [SerializeField] private Structures.ModuleBuildingData[] structuresModulesData;
//         [SerializeField] private Structures.BlockBuildingData[] structuresBlocksData;
//
//         [SerializeField] private Units.ModuleBuildingData[] unitsModulesData;
//
//
//         public Structures.ModuleBuildingData[] StructuresModulesData => structuresModulesData;
//         public Structures.BlockBuildingData[] StructuresBlocksData => structuresBlocksData;
//
//         public Units.ModuleBuildingData[] UnitsModulesData => unitsModulesData;
//
//
//         public Structures.ModuleBuildingData GetStructureModuleData(int id)
//         {
//             return id < structuresModulesData.Length ? structuresModulesData[id] : null;
//         }
//
//         public Structures.BlockBuildingData GetStructureBlock(int id)
//         {
//             return id < structuresBlocksData.Length ? structuresBlocksData[id] : null;
//         }
//
//         public Units.ModuleBuildingData GetUnitModuleData(int id)
//         {
//             return id < unitsModulesData.Length ? unitsModulesData[id] : null;
//         }
//
//
//         public int GetIndex(Structures.Module structureModule)
//         {
//             string moduleName = structureModule.gameObject.name;
//             moduleName = moduleName.Remove(moduleName.IndexOf(("(Clone)")));
//
//             for (int i = 0; i < structuresModulesData.Length; i++)
//             {
//                 if (structuresModulesData[i].Prefab.name == moduleName)
//                 {
//                     return i;
//                 }
//             }
//
//             return -1;
//         }
//
//         public int GetIndex(Structures.Block structureBlock)
//         {
//             string blockName = structureBlock.gameObject.name;
//             blockName = blockName.Remove(blockName.IndexOf(("(Clone)")));
//
//             for (int i = 0; i < structuresBlocksData.Length; i++)
//             {
//                 if (structuresBlocksData[i].Prefab.name == blockName)
//                 {
//                     return i;
//                 }
//             }
//
//             return -1;
//         }
//
//         public int GetIndex(Units.ModuleBuildingData unitModule)
//         {
//             for (int i = 0; i < unitsModulesData.Length; i++)
//             {
//                 if (unitsModulesData[i].name == unitModule.name)
//                 {
//                     return i;
//                 }
//             }
//
//             return -1;
//         }
//     }
// }