// using UnityEngine;
//
// public class UnitSpawner : MonoBehaviour
// {
//     [SerializeField] private string unitTeam;
//     [SerializeField] private Structure destinationStructure;
//     [SerializeField] private Transform spawnPoint;
//     [SerializeField] private MovementPath movementPath;
//
//
//     void OnUnitHealthChanged(Unit unit)
//     {
//         Debug.Log("HEALTH : " + unit.Health);
//     }
//
//     void OnUnitDie(Unit unit)
//     {
//         Debug.Log(unit.gameObject.name + " IS DEAD");
//     }
//
//
//     public SimpleUnitBehaviour unit;
//     void Start()
//     {
//         if (unit)
//         {
//             unit.Init(ETeam.Player, movementPath, destinationStructure);
//         }
//     }
//
//     public void Spawn(Unit prefab)
//     {
//         Unit unit = Instantiate(prefab, spawnPoint.position, Quaternion.identity);
//         unit.Init(unitTeam, destinationStructure, movementPath);
//         unit.OnHealthChanged += OnUnitHealthChanged;
//         unit.OnDie += OnUnitDie;
//     }
//
//     public void SetDestination(Structure newDestinationStructure)
//     {
//         destinationStructure = newDestinationStructure;
//     }
//
//     public void SetSpawnPoint(Transform newSpawnPoint)
//     {
//         spawnPoint = newSpawnPoint;
//     }
//
//     public void SetMovementPath(MovementPath path)
//     {
//         movementPath = path;
//     }
// }