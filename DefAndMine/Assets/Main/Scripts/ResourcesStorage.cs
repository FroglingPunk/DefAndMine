public class ResourcesStorage
{
    public int Money { get; private set; }


    public ResourcesStorage(int startMoney)
    {
        Money = startMoney;
    }


    public void Increase(int delta)
    {
        Money += delta;
    }

    public bool TryDecrease(int delta)
    {
        if (CanAfford(delta))
        {
            Money -= delta;
            return true;
        }

        return false;
    }

    public bool CanAfford(int cost)
    {
        return Money >= cost;
    }
}