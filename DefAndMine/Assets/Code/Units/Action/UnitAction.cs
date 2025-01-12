using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public abstract class UnitAction : ScriptableObject, IUnitAction
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }


    public abstract UniTask<UnitActionContext> ManualCreateContextAsync(Unit unit, CancellationToken cancellationToken);
    public abstract UniTask<UnitActionContext> AICreateContextAsync(Unit unit);
    public abstract UniTask ExecuteAsync(UnitActionContext context);
    public abstract void SetupMarksForDeferredAction(DeferredAction deferredAction, ref CompositeDisposable disposables, ref List<GameObject> marksGo);
}