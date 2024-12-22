// using UnityEngine;
// using System;
//
// public class BattleManager : MonoBehaviour
// {
//     public EBattleState State { get; private set; }
//
//     [SerializeField] private UnitSpawnerManager unitSpawnerManager;
//     [SerializeField] private BattleData battleData;
//
//     private UnitSpawnLogic[] unitsSpawnerLogic;
//     public int CurrentWaveID { get; private set; }
//
//     public event Action<int> OnWaveStarted;
//     public event Action<int> OnWaveCompleted;
//     public event Action<EBattleState> OnStateChanged;
//
//
//     void Start()
//     {
//         ChangeState(EBattleState.Spawn);
//     }
//
//     void Update()
//     {
//         if (State == EBattleState.Spawn)
//         {
//             bool allUnitsSpawned = true;
//
//             for (int i = 0; i < unitsSpawnerLogic.Length; i++)
//             {
//                 if (unitsSpawnerLogic[i].Update(Time.deltaTime) != UnitSpawnLogic.EUnitSpawnLogicState.Completed)
//                 {
//                     allUnitsSpawned = false;
//                 }
//             }
//
//             if (allUnitsSpawned)
//             {
//                 ChangeState(EBattleState.WaitingForUnitsDeath);
//             }
//         }
//         else if(State == EBattleState.WaitingForUnitsDeath)
//         {
//             bool allUnitsDead = true;
//
//             for (int i = 0; i < unitsSpawnerLogic.Length; i++)
//             {
//                 if (unitsSpawnerLogic[i].State != UnitSpawnLogic.EUnitSpawnLogicState.Completed)
//                 {
//                     allUnitsDead = false;
//                 }
//             }
//
//             if (allUnitsDead)
//             {
//                 ChangeState(EBattleState.Spawn);
//             }
//         }
//     }
//
//
//     private void StartWave(int waveID)
//     {
//         CurrentWaveID = waveID;
//
//         if (CurrentWaveID >= battleData.UnitWaveSpawnData.Length)
//         {
//             ChangeState(EBattleState.Completed);
//             return;
//         }
//
//         UnitWaveSpawnData wave = battleData.UnitWaveSpawnData[waveID];
//         unitsSpawnerLogic = new UnitSpawnLogic[wave.SpawnerUnitSpawnData.Length];
//
//         for (int i = 0; i < wave.SpawnerUnitSpawnData.Length; i++)
//         {
//             unitsSpawnerLogic[i] =
//                 new UnitSpawnLogic().Init(
//                     wave.SpawnerUnitSpawnData[i].SpawnData,
//                     unitSpawnerManager.GetSpawner(wave.SpawnerUnitSpawnData[i].SpawnerID)
//                     );
//         }
//
//         OnWaveStarted?.Invoke(CurrentWaveID);
//     }
//
//     private void NextWave()
//     {
//         StartWave(CurrentWaveID + 1);
//     }
//
//     private void ChangeState(EBattleState newState)
//     {
//         if (State == newState)
//         {
//             return;
//         }
//
//         EBattleState previousState = State;
//         State = newState;
//
//         switch (State)
//         {
//             case EBattleState.Spawn:
//                 if (previousState == EBattleState.NotInit)
//                 {
//                     StartWave(0);
//                 }
//                 else if (previousState == EBattleState.WaitingForUnitsDeath)
//                 {
//                     OnWaveCompleted?.Invoke(CurrentWaveID);
//                     NextWave();
//                 }
//                 break;
//
//             case EBattleState.WaitingForUnitsDeath:
//                 
//                 break;
//
//             case EBattleState.Completed:
//                 Debug.Log("COMPLETED");
//                 break;
//         }
//
//         OnStateChanged?.Invoke(newState);
//     }
// }