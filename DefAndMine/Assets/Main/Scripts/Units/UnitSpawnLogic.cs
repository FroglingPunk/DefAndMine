public class UnitSpawnLogic
{
    public enum EUnitSpawnLogicState
    {
        WaitingForInit,
        WaitingDelayBeforeStart,
        SpawnActive,
        SpawnInactive,
        Completed
    }

    public EUnitSpawnLogicState State { get; private set; }
    private UnitSpawnData data;
    private UnitSpawner spawner;

    private float delayBeforeStartCompleted;
    private float delayBeforeSpawnCompleted;
    private int unitsSpawned;


    public UnitSpawnLogic Init(UnitSpawnData data, UnitSpawner spawner)
    {
        this.data = data;
        this.spawner = spawner;

        if (data.Amount < 1)
        {
            State = EUnitSpawnLogicState.Completed;
        }
        else
        {
            State = EUnitSpawnLogicState.WaitingDelayBeforeStart;
        }

        return this;
    }

    public EUnitSpawnLogicState Update(float deltaTime)
    {
        if (State == EUnitSpawnLogicState.WaitingDelayBeforeStart)
        {
            delayBeforeStartCompleted += deltaTime;

            if (delayBeforeStartCompleted >= data.DelayBeforeStart)
            {
                State = EUnitSpawnLogicState.SpawnActive;
            }
        }
        else if (State == EUnitSpawnLogicState.SpawnActive)
        {
            delayBeforeSpawnCompleted += deltaTime;

            if (delayBeforeSpawnCompleted >= data.DelayBetweenSpawn)
            {
                delayBeforeSpawnCompleted = 0;
                spawner.Spawn(data.UnitPrefab);
                unitsSpawned++;

                if (unitsSpawned >= data.Amount)
                {
                    State = EUnitSpawnLogicState.Completed;
                }
            }
        }

        return State;
    }
}