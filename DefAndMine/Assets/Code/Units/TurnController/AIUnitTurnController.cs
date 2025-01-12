using Cysharp.Threading.Tasks;

public class AIUnitTurnController : IUnitTurnController
{
    public async UniTask ExecuteAsync()
    {
        var units = BattleTurnSystem.Instance.UnitsList.GetUnitsByTeam(ETeam.Enemy);

        for (var i = 0; i < units.Count; i++)
        {
            var unit = units[i];

            if (unit.Context.BaseMoveAction != null)
            {
                var possibleForMoveCells = unit.Cell.Value.Echo(unit, unit.MovementDistance);

                if (possibleForMoveCells.Count > 0)
                {
                    var playerUnits = BattleTurnSystem.Instance.UnitsList.GetUnitsByTeam(ETeam.Player);
                    var closestCell = possibleForMoveCells[0];

                    for (var p = 1; p < possibleForMoveCells.Count; p++)
                    {
                        playerUnits.ForEach(playerUnit =>
                        {
                            if (playerUnit.Cell.Value.Distance(possibleForMoveCells[p]) < playerUnit.Cell.Value.Distance(closestCell))
                            {
                                closestCell = possibleForMoveCells[p];
                            }
                        });
                    }

                    if (Pathfinder.TryBuildPath(unit.Cell.Value, closestCell, unit, out var path))
                    {
                        await unit.MoveAsync(path);
                    }
                }
            }
          
            var action = unit.Context.Actions.GetRandom();
            var context = await action.AICreateContextAsync(unit);

            if (context != null)
            {
                BattleTurnSystem.Instance.DeferredActionsList.AddAction(new DeferredAction(action, context,
                    EBattleStage.NonPlayerTurnExecution));
            }

            await UniTask.WaitForSeconds(0.5f);
        }
    }
}