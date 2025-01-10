using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class BattleTurnSystem
{
    public static BattleTurnSystem Instance { get; private set; }

    public ReactiveProperty<int> RoundNumber { get; private set; } = new();
    public ReactiveProperty<EBattleStage> Stage { get; private set; } = new();


    private Unit[] _unitTemplates;
    private BattleActorsList _actorsList;


    public BattleTurnSystem(Unit[] unitTemplates)
    {
        Instance = this;
        _unitTemplates = unitTemplates;
    }

    public void StartBattle()
    {
        var units = new List<Unit>();

        for (var i = 0; i < _unitTemplates.Length; i++)
        {
            var template = _unitTemplates[i];

            for (var p = 0; p < 1; p++)
            {
                var unit = Object.Instantiate(template);
                var team = (ETeam)i;
                var cell = GetRandomFreeCell();
                unit.Init(team, cell);
                units.Add(unit);
            }
        }

        _actorsList = new(units);
        _ = BattleCycle();


        Cell GetRandomFreeCell()
        {
            for (var i = 0; i < Field.Width * Field.Length; i++)
            {
                var x = Random.Range(0, Field.Width);
                var z = Random.Range(0, Field.Length);
                var cell = Field.Instance[x, z];

                if (units.Find(u => u.Cell == cell) == null)
                {
                    return cell;
                }
            }

            return null;
        }
    }

    private async UniTask BattleCycle()
    {
        while (true)
        {
            RoundNumber.Value++;
            await RoundCycle();
        }
    }

    private async UniTask RoundCycle()
    {
        for (var stage = EBattleStage.RoundStart; stage <= EBattleStage.RoundEnd; stage++)
        {
            Stage.Value = stage;
            var actors = _actorsList.GetActorsByStage(stage);

            if (actors == null || actors.Count == 0)
            {
                continue;
            }

            for (var i = 0; i < actors.Count; i++)
            {
                await actors[i].ExecuteTurnAsync(stage);
            }

            await UniTask.WaitForSeconds(1f);
        }
    }
}