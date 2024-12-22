// using UnityEngine;
// using Constructor.Structures;
//
// namespace Constructor.Utils
// {
//     [RequireComponent(typeof(Structure))]
//     public class AutoInitStructureOnStart : MonoBehaviour
//     {
//         [SerializeField] private Structure structure;
//         [SerializeField] private string team;
//         [SerializeField] private Cell[] occupiedCells;
//
//         public bool IsInit { get; private set; }
//
//
//         void OnValidate()
//         {
//             if (structure == null)
//             {
//                 structure = GetComponent<Structure>();
//
//                 if (structure == null)
//                 {
//                     structure = GetComponentInChildren<Structure>();
//                 }
//             }
//         }
//
//         void Start()
//         {
//             structure.Init(occupiedCells);
//             IsInit = true;
//         }
//     }
// }