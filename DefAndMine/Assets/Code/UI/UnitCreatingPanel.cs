// using UIElementsList;
// using UnityEngine;
//
// public class UnitCreatingPanel : MonoBehaviour
// {
//     [SerializeField] private GameObject panelGameobject;
//     [SerializeField] private UIList uIList;
//     // debug only
//     [SerializeField] private Unit[] unitsPatterns;
//     //
//
//
//     // void Awake()
//     // {
//     //     Control.Instance.OnCancelButtonClick += OnCancelButtonClick;
//     // }
//     //
//     // void OnDestroy()
//     // {
//     //     Control.Instance.OnCancelButtonClick -= OnCancelButtonClick;
//     // }
//
//
//     private void OnCancelButtonClick()
//     {
//         Hide();
//     }
//
//
//     public void Show()
//     {
//         panelGameobject.SetActive(true);
//
//         uIList.Init(unitsPatterns, null, null, null);
//     }
//
//     public void Hide()
//     {
//         panelGameobject.SetActive(false);
//     }
// }