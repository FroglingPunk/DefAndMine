// using System.Collections.Generic;
// using UnityEngine;
// using System;
//
// namespace Constructor.Utils
// {
//     [RequireComponent(typeof(SphereCollider))]
//     public class InfluenceArea : MonoBehaviour
//     {
//         public List<Unit> UnitsInside { get; private set; }
//         public Unit GetRandomUnit()
//         {
//             if (UnitsInside.Count > 0)
//             {
//                 return UnitsInside[UnityEngine.Random.Range(0, UnitsInside.Count)];
//             }
//
//             return null;
//         }
//
//         public Unit GetRandomUnit(string team)
//         {
//             List<Unit> teamUnits = new List<Unit>();
//             for(int i = 0; i < UnitsInside.Count; i++)
//             {
//                 if(UnitsInside[i].Team == team)
//                 {
//                     teamUnits.Add(UnitsInside[i]);
//                 }
//             }
//
//             if (teamUnits.Count > 0)
//             {
//                 return teamUnits[UnityEngine.Random.Range(0, teamUnits.Count)];
//             }
//
//             return null;
//         }
//
//         public event Action<Unit> OnUnitEnter;
//         public event Action<Unit> OnUnitExit;
//
//
//         void Awake()
//         {
//             UnitsInside = new List<Unit>();
//         }
//
//         void OnTriggerEnter(Collider other)
//         {
//             Unit unit = null;
//             if (other.TryGetComponent(out unit))
//             {
//                 OnUnitEnter?.Invoke(unit);
//                 UnitsInside.Add(unit);
//                 unit.OnDie += OnUnitDie;
//             }
//         }
//
//         void OnTriggerExit(Collider other)
//         {
//             Unit unit = null;
//             if (other.TryGetComponent(out unit))
//             {
//                 OnUnitExit?.Invoke(unit);
//                 UnitsInside.Remove(unit);
//                 unit.OnDie += OnUnitDie;
//             }
//         }
//
//
//         private void OnUnitDie(Unit unit)
//         {
//             unit.OnDie -= OnUnitDie;
//             UnitsInside.Remove(unit);
//             OnUnitExit?.Invoke(unit);
//         }
//     }
// }