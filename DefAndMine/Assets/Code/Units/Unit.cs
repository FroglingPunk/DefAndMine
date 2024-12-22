// using System;
// using UnityEngine;
// using UnityEngine.AI;
// using Constructor.Utils;
// using System.Collections.Generic;
// using System.Collections;
// using Constructor.Units;
//
// [RequireComponent(typeof(NavMeshAgent))]
// public class Unit : MonoBehaviour
// {
//     public string Team { get; private set; }
//     public EUnitBehaviour Behaviour { get; private set; }
//
//     private Structure targetStructure;
//     private Unit targetUnit;
//
//     private UnitBody body;
//
//
//     [SerializeField] private NavMeshAgent navMeshAgent;
//     [SerializeField] private InfluenceArea visibleArea;
//     [SerializeField] private int maxHealth = 30;
//     [SerializeField] private int damage = 6;
//     [SerializeField] private float attackDelay = 3f;
//     [SerializeField] private SerializableResourcesPackage drop;
//
//     private float timeFromLastAttack = 0f;
//
//     private int health;
//     public int Health
//     {
//         get => health;
//         set
//         {
//             health = value;
//
//             OnHealthChanged?.Invoke(this);
//
//             if (health == 0)
//             {
//                 OnDie?.Invoke(this);
//             }
//         }
//     }
//
//     public SerializableResourcesPackage Drop => drop;
//
//     public event Action<Unit> OnHealthChanged;
//     public event Action<Unit> OnDie;
//
//
//     void OnValidate()
//     {
//         if (visibleArea == null)
//         {
//             visibleArea = GetComponent<InfluenceArea>();
//
//             if (visibleArea == null)
//             {
//                 visibleArea = GetComponentInChildren<InfluenceArea>();
//             }
//         }
//     }
//
//     void Start()
//     {
//         if (visibleArea)
//         {
//             visibleArea.OnUnitEnter += OnUnitEnterToVisibleArea;
//         }
//     }
//
//     void Update()
//     {
//         if (Behaviour == EUnitBehaviour.Battle)
//         {
//             Debug.DrawLine(transform.position, targetUnit.transform.position, Color.red, Time.deltaTime);
//
//             timeFromLastAttack += Time.deltaTime;
//
//             if (timeFromLastAttack >= attackDelay)
//             {
//                 timeFromLastAttack = 0f;
//                 targetUnit.GetDamage(damage);
//             }
//         }
//         else
//         {
//             List<Unit> unitsInVisibleArea = visibleArea.UnitsInside;
//             for(int i = 0; i < unitsInVisibleArea.Count; i++)
//             {
//                 Unit unit = unitsInVisibleArea[i];
//                 if (Behaviour == EUnitBehaviour.Taking)
//                 {
//                     if (unit.Team != Team && unit.Behaviour == EUnitBehaviour.Taking)
//                     {
//                         SetTarget(unit);
//                         break;
//                     }
//                 }
//             }
//         }
//     }
//
//     void OnDestroy()
//     {
//         if (visibleArea)
//         {
//             visibleArea.OnUnitEnter -= OnUnitEnterToVisibleArea;
//         }
//     }
//
//
//     private void SetTarget(Unit unit)
//     {
//         Behaviour = EUnitBehaviour.Battle;
//         unit.Behaviour = EUnitBehaviour.Battle;
//
//         targetUnit = unit;
//         unit.targetUnit = this;
//
//         Vector3 battlePoint = Vector3.Lerp(transform.position, unit.transform.position, 0.5f);
//         navMeshAgent.SetDestination(battlePoint);
//         unit.navMeshAgent.SetDestination(battlePoint);
//         
//         unit.OnDie += OnTargetUnitDie;
//         OnDie += unit.OnTargetUnitDie;
//
//         timeFromLastAttack = 0;
//         unit.timeFromLastAttack = 0f;
//     }
//
//     private void SetTarget(Structure structure)
//     {
//         Behaviour = EUnitBehaviour.Taking;
//         targetStructure = structure;
//     }
//
//     private void OnTargetUnitDie(Unit unit)
//     {
//         OnDie -= unit.OnTargetUnitDie;
//         SetTarget(targetStructure);
//     }
//
//     private void OnUnitEnterToVisibleArea(Unit unit)
//     {
//         if (Behaviour == EUnitBehaviour.Taking)
//         {
//             if (unit.Team != Team && unit.Behaviour == EUnitBehaviour.Taking)
//             {
//                 SetTarget(unit);
//             }
//         }
//     }
//
//
//     public void Init(string team, Structure targetStructure, MovementPath path)
//     {
//         Team = team;
//         health = maxHealth;
//
//         body = GetComponent<UnitBody>();
//         body.Init(this);
//
//         StartCoroutine(Movement(path));
//
//         SetTarget(targetStructure);
//     }
//
//
//     public void Die()
//     {
//         Health = 0;
//
//         Destroy(gameObject);
//     }
//
//     public void GetDamage(int damage)
//     {
//         if (Health > damage)
//         {
//             Health -= damage;
//         }
//         else
//         {
//             Die();
//         }
//     }
//
//
//     IEnumerator Movement(MovementPath path)
//     {
//         EUnitBehaviour previousBehaviour = Behaviour;
//         int currentPathCellID = 0;
//
//         // navMeshAgent.SetDestination(path.cells[currentPathCellID].transform.position);
//
//         while (true)
//         {
//             if (Behaviour == EUnitBehaviour.Taking)
//             {
//                 if (currentPathCellID < path.cells.Length)
//                 {
//                     if (previousBehaviour == EUnitBehaviour.Battle)
//                     {
//                         // navMeshAgent.SetDestination(path.cells[currentPathCellID].transform.position);
//                     }
//
//                     // float distance = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(path.cells[currentPathCellID].transform.position.x, path.cells[currentPathCellID].transform.position.z));
//
//                     // if (distance < 2f)
//                     // {
//                     //     currentPathCellID++;
//                     //
//                     //     if (currentPathCellID < path.cells.Length)
//                     //     {
//                     //         // navMeshAgent.SetDestination(path.cells[currentPathCellID].transform.position);
//                     //     }
//                     // }
//                 }
//             }
//             else if (Behaviour == EUnitBehaviour.Battle)
//             {
//
//             }
//
//             previousBehaviour = Behaviour;
//             yield return null;
//         }
//     }
// }