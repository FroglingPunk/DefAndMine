using System.Threading;
using Cysharp.Threading.Tasks;

public interface IUnitAction
{
    UniTask<UnitActionContext> CreateContextAsync(Unit unit, CancellationToken cancellationToken);
    UniTask ExecuteAsync(UnitActionContext context);
}