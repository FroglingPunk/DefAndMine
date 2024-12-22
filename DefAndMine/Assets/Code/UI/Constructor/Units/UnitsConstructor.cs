// using Constructor.Units;
// using UnityEngine;
// using UnityEngine.UI;
//
// namespace UI.Constructor.Units
// {
//     public class UnitsConstructor : MonoBehaviour
//     {
//         // debug
//         [SerializeField] private Transform unitSpawnPlace;
//         private Unit lastUnit;
//
//         [SerializeField] private ModulesPanel modulesPanel;
//
//         [SerializeField] private Button buttonClose;
//         [SerializeField] private Button buttonSave;
//
//
//         void Awake()
//         {
//             buttonClose.onClick.AddListener(Close);
//             buttonSave.onClick.AddListener(Save);
//
//             modulesPanel.OnModuleChanged += OnModuleChanged;
//         }
//
//
//         private void OnModuleChanged(ModuleBuildingData module)
//         {
//             UpdateUnit();
//         }
//
//         private void UpdateUnit()
//         {
//             if(lastUnit != null)
//             {
//                 Destroy(lastUnit.gameObject);
//             }
//
//             lastUnit = UnitBuildingDataSerializator.Serialize(modulesPanel.Modules).CreateInstance();
//             lastUnit.enabled = false;
//             lastUnit.transform.SetParent(unitSpawnPlace);
//             lastUnit.transform.localPosition = Vector3.zero;
//         }
//
//         private void Close()
//         {
//
//         }
//
//         private void Save()
//         {
//             UnitBuildingDataSerializator.Write(UnitBuildingDataSerializator.Serialize(modulesPanel.Modules));
//         }
//     }
// }