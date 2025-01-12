using System.Threading;
using Cysharp.Threading.Tasks;

public interface IUnitAction
{
    UniTask<UnitActionContext> ManualCreateContextAsync(Unit unit, CancellationToken cancellationToken);
    UniTask<UnitActionContext> AICreateContextAsync(Unit unit);
    UniTask ExecuteAsync(UnitActionContext context);
}