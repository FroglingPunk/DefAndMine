using Cysharp.Threading.Tasks;
using UnityEngine;

public class AIUnitTurnController : IUnitTurnController
{
    public async UniTask ExecuteAsync(Unit unit)
    {
        Debug.Log("AI Turn Start");
        await UniTask.WaitForSeconds(0.5f);
        Debug.Log("AI Turn Finish");
    }
}