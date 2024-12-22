// using System.Collections;
// using UnityEngine;
//
// public class Polly : MonoBehaviour
// {
//     [SerializeField] private ResourcesDropData resourcesDropData;
//     [SerializeField] private BattleManager battleManager;
//
//     public ResourcesStorage ResourcesStorage { get; private set; }
//
//
//     void Awake()
//     {
//         ResourcesStorage = new ResourcesStorage(resourcesDropData.StartResources.All);
//
//         battleManager.OnWaveStarted += OnWaveStarted;
//         battleManager.OnWaveCompleted += OnWaveCompleted;
//         battleManager.OnStateChanged += OnBattleStateChanged;
//     }
//
//
//     private void Drop(int waveID)
//     {
//         ResourcesStorage.Increase(resourcesDropData.Drop.All);
//     }
//
//     private void OnWaveStarted(int waveID)
//     {
//         if (waveID == 0)
//         {
//             if (resourcesDropData.EResourcesDropType == EResourcesDropType.TimeByTime)
//             {
//                 StartCoroutine(DropTimeByTime(resourcesDropData.DelayBetweenDrops));
//             }
//         }
//     }
//
//     private void OnWaveCompleted(int waveID)
//     {
//         if (resourcesDropData.EResourcesDropType == EResourcesDropType.EveryWave)
//         {
//             Drop(waveID);
//         }
//     }
//
//     private void OnBattleStateChanged(EBattleState state)
//     {
//         if (state == EBattleState.Completed)
//         {
//             battleManager.OnWaveStarted -= OnWaveStarted;
//             battleManager.OnWaveCompleted -= OnWaveCompleted;
//             battleManager.OnStateChanged -= OnBattleStateChanged;
//
//             //StopAllCoroutines();
//         }
//     }
//
//
//     private IEnumerator DropTimeByTime(float delay)
//     {
//         WaitForSeconds wait = new WaitForSeconds(delay);
//
//         while (true)
//         {
//             yield return wait;
//             Drop(battleManager.CurrentWaveID);
//         }
//     }
// }