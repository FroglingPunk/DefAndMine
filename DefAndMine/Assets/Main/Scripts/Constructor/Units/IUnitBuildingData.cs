namespace Constructor.Units
{
    public interface IUnitBuildingData
    {
        ResourcesStorage Cost { get; }


        Unit CreateInstance();
    }
}