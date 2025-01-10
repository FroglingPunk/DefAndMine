using System.Collections.Generic;

public class BattleActorsList
{
    private readonly List<IBattleActor> _actors;


    public BattleActorsList(IEnumerable<IBattleActor> actors)
    {
        _actors = new List<IBattleActor>(actors);
    }

    public void AddActor(IBattleActor actor)
    {
        _actors.Add(actor);
    }

    public void RemoveActor(IBattleActor actor)
    {
        _actors.Remove(actor);
    }

    public List<IBattleActor> GetActorsByStage(EBattleStage stage)
    {
        var actors = _actors.FindAll(a => a.IsSupportStage(stage));
        actors.Sort((x, y) => x.Priority - y.Priority);
        return actors;
    }
}