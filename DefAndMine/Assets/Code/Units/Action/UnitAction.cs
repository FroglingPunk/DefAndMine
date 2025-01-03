using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class UnitAction : ScriptableObject, IUnitAction
{
    [field: SerializeField] public Sprite Icon { get; private set; }
    [field: SerializeField] public string Name { get; private set; }


    public abstract UniTask<UnitActionContext> CreateContextAsync(Unit unit, CancellationToken cancellationToken);
    public abstract UniTask ExecuteAsync(UnitActionContext context);
}