// using UnityEngine;
// using System;
// using UnityEngine.EventSystems;
//
// public class Control : MonoBehaviour
// {
//     [SerializeField] private Camera mainCamera;
//
//     public event Action<Cell> OnCellChosen;
//     public event Action<Structure> OnStructureChosen;
//     public event Action<Cell, Cell> OnCursorCellChanged;    // previous Cell, current Cell
//
//     public event Action OnCancelButtonClick;
//
//     public Structure ChosenStructure { get; private set; }
//     public Cell ChosenCell { get; private set; }
//     public Cell CursorCell { get; private set; }
//
//
//     void Awake()
//     {
//         if (mainCamera == null)
//         {
//             mainCamera = Camera.main;
//         }
//     }
//
//     void Update()
//     {
//         if (!EventSystem.current.IsPointerOverGameObject())
//         {
//             RaycastHit hit;
//             Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
//
//             if (Physics.Raycast(ray, out hit))
//             {
//                 Cell cell;
//                 if (hit.collider.TryGetComponent(out cell))
//                 {
//                     if (CursorCell != cell)
//                     {
//                         OnCursorCellChanged?.Invoke(CursorCell, cell);
//                         CursorCell = cell;
//                     }
//                 }
//                 else
//                 {
//                     // if (CursorCell)
//                     // {
//                     //     OnCursorCellChanged?.Invoke(CursorCell, null);
//                     //     CursorCell = null;
//                     // }
//                 }
//
//                 if (Input.GetMouseButtonDown(0))
//                 {
//                     if (hit.collider.TryGetComponent(out cell))
//                     {
//                         ChosenCell = cell;
//                         OnCellChosen?.Invoke(cell);
//                     }
//
//                     Structure structure;
//                     if (hit.collider.TryGetComponent(out structure))
//                     {
//                         ChosenStructure = structure;
//                         OnStructureChosen?.Invoke(structure);
//                     }
//                 }
//             }
//             else
//             {
//                 // if (CursorCell)
//                 // {
//                 //     OnCursorCellChanged?.Invoke(CursorCell, null);
//                 //     CursorCell = null;
//                 // }
//             }
//         }
//
//         if (Input.GetMouseButtonDown(1))
//         {
//             ChosenCell = null;
//             ChosenStructure = null;
//
//             OnCancelButtonClick?.Invoke();
//         }
//     }
// }