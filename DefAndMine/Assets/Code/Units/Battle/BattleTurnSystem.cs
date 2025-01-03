using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class BattleTurnSystem
{
    private List<Unit> _units;
    private int _currentRound;

    private IUnitTurnController _playerTurnController = new PlayerUnitTurnController();
    private IUnitTurnController _aiUnitTurnController = new AIUnitTurnController();

    private Unit[] _unitTemplates;

    public BattleTurnSystem(Unit[] unitTemplates)
    {
        _unitTemplates = unitTemplates;
    }

    public void StartBattle()
    {
        Cell GetRandomFreeCell()
        {
            for (var i = 0; i < Field.Instance.Width * Field.Instance.Length; i++)
            {
                var x = Random.Range(0, Field.Instance.Width);
                var z = Random.Range(0, Field.Instance.Length);
                var cell = Field.Instance[x, z];

                if (_units.Find(u => u.Cell == cell) == null)
                {
                    return cell;
                }
            }

            return null;
        }

        _units = new List<Unit>();

        for (var i = 0; i < _unitTemplates.Length; i++)
        {
            var template = _unitTemplates[i];

            for (var p = 0; p < 2; p++)
            {
                var unit = Object.Instantiate(template);
                var team = (ETeam)i;
                var cell = GetRandomFreeCell();
                unit.Init(team, cell);
                _units.Add(unit);
            }
        }

        _ = BattleCycle();
    }

    private async UniTask BattleCycle()
    {
        while (true)
        {
            _currentRound++;
            await RoundCycle();
        }
    }

    private async UniTask RoundCycle()
    {
        var neutralEnemyUnits = _units.Where(u => u.Team == ETeam.Enemy || u.Team == ETeam.Neutral).ToList();
        neutralEnemyUnits.Sort((u1, u2) => u1.MovePriority - u2.MovePriority);

        for (var i = 0; i < neutralEnemyUnits.Count; i++)
        {
            var unit = neutralEnemyUnits[i];
            await _aiUnitTurnController.ExecuteAsync(unit);
        }

        var playerUnits = _units.Where(u => u.Team == ETeam.Player).ToList();

        for (var i = 0; i < playerUnits.Count; i++)
        {
            var unit = playerUnits[i];
            await _playerTurnController.ExecuteAsync(unit);
        }

        for (var i = 0; i < neutralEnemyUnits.Count; i++)
        {
            var unit = neutralEnemyUnits[i];
            await _aiUnitTurnController.ExecuteAsync(unit);
        }
    }
}