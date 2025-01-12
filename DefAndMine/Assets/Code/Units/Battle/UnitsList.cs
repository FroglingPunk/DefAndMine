using System.Collections.Generic;

public class UnitsList
{
    private readonly List<Unit> _units = new();


    public UnitsList(IEnumerable<Unit> units = null)
    {
        _units = units == null ? new() : new(units);
    }

    public void AddAUnit(Unit unit)
    {
        _units.Add(unit);
    }

    public void RemoveUnit(Unit unit)
    {
        _units.Remove(unit);
    }

    public List<Unit> GetUnitsByTeam(ETeam team)
    {
        return _units.FindAll(u => u.Team == team && u.IsAlive);
    }
}