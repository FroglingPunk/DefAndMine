using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class BattleTurnSystem
{
    public static BattleTurnSystem Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BattleTurnSystem();
            }

            return _instance;
        }
    }
    private static BattleTurnSystem _instance;

    public ReactiveProperty<int> RoundNumber { get; private set; } = new();
    public ReactiveProperty<EBattleStage> Stage { get; private set; } = new();

    public DeferredActionsList DeferredActionsList { get; private set; } = new();
    public UnitsList UnitsList { get; private set; } = new();
    
    private AIUnitTurnController _aiTurnController = new();
    private PlayerUnitTurnController _playerTurnController = new();

    private BattleData _battleData;


    public void StartBattle(BattleData battleData)
    {
        _battleData = battleData;

        _ = BattleCycle();
    }

    private async UniTask PlayerUnitsSpawnAsync()
    {
        var possibleCells = new List<Cell>();

        _battleData.playerUnitsPossibleSpawnCells.ForEach(id =>
        {
            var x = id % Field.Width;
            var z = id / Field.Width;
            var cell = Field.Instance[x, z];
            cell.SetHighlightState(EHighlightState.PossibleTarget);
            possibleCells.Add(cell);
        });

        var raycastPointer = new RaycastPointer<CellView>();
        var selectedCell = (Cell)default;

        raycastPointer.OnClick.Subscribe(cellView =>
        {
            if (possibleCells.Contains(cellView.Cell))
            {
                selectedCell = cellView.Cell;
            }
        });

        for (var i = 0; i < _battleData.playerUnitsTemplates.Count; i++)
        {
            selectedCell = null;

            while (selectedCell == null)
            {
                await UniTask.Yield();
            }

            selectedCell.SetHighlightState(EHighlightState.None);
            possibleCells.Remove(selectedCell);
            
            var unit = Object.Instantiate(_battleData.playerUnitsTemplates[i]);
            unit.Init(ETeam.Player, selectedCell);
            UnitsList.AddAUnit(unit);
        }
        
        Field.Instance.SetCellsHighlightState(EHighlightState.None);
    }

    private async UniTask EnemyUnitsSpawnAsync()
    {
        for (var i = 0; i < _battleData.enemyUnitsSpawnCells.Count; i++)
        {
            var id = _battleData.enemyUnitsSpawnCells[i];
            var x = id % Field.Width;
            var z = id / Field.Width;
            var cell = Field.Instance[x, z];
            
            var unit = Object.Instantiate(_battleData.enemyUnitsTemplates[i]);
            unit.Init(ETeam.Enemy, cell);
            UnitsList.AddAUnit(unit);
            
            await UniTask.Yield();
        }
    }
    
    private async UniTask BattleCycle()
    {
        await EnemyUnitsSpawnAsync();
        await PlayerUnitsSpawnAsync();

        while (true)
        {
            RoundNumber.Value++;
            await RoundCycle();
        }
    }

    private async UniTask RoundCycle()
    {
        Stage.Value = EBattleStage.RoundStart;
        await ExecuteStageActionsAsync(EBattleStage.RoundStart);
        await UniTask.WaitForSeconds(0.2f);

        Stage.Value = EBattleStage.NonPlayerTurnPreparing;
        await _aiTurnController.ExecuteAsync();
        await UniTask.WaitForSeconds(0.2f);

        Stage.Value = EBattleStage.PlayerTurn;
        await _playerTurnController.ExecuteAsync();
        await UniTask.WaitForSeconds(0.2f);

        Stage.Value = EBattleStage.NonPlayerTurnExecution;
        await ExecuteStageActionsAsync(EBattleStage.NonPlayerTurnExecution);
        await UniTask.WaitForSeconds(0.2f);

        Stage.Value = EBattleStage.RoundEnd;
        await ExecuteStageActionsAsync(EBattleStage.RoundEnd);
        await UniTask.WaitForSeconds(0.2f);
    }

    private async UniTask ExecuteStageActionsAsync(EBattleStage stage)
    {
        var stageActions = DeferredActionsList.GetActionsByStage(stage);

        if (stageActions == null || stageActions.Count == 0)
        {
            return;
        }
        
        for (var i = 0; i < stageActions.Count; i++)
        {
            var action = stageActions[i];
            await action.ExecuteAsync();
        }
    }
}