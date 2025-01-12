using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

[CreateAssetMenu(menuName = "Unit/Actions/Skip", fileName = "SkipAction", order = 0)]
public class SkipAction : UnitAction
{
    public override async UniTask<UnitActionContext> ManualCreateContextAsync(Unit unit, CancellationToken cancellationToken)
    {
        return new UnitActionContext
        {
            sourceCell = unit.Cell.Value,
            sourceUnit = unit
        };
    }
    
    public override async UniTask<UnitActionContext> AICreateContextAsync(Unit unit)
    {
        return default;
    }

    public override async UniTask ExecuteAsync(UnitActionContext context)
    {
        await UniTask.Yield();
    }
    
    public override void SetupMarksForDeferredAction(DeferredAction deferredAction, ref CompositeDisposable disposables, ref List<GameObject> marksGo)
    {
        
    }
}