using System.Collections.Generic;

[System.Serializable]
public class BattleData
{
    public FieldData fieldData;
    public List<Unit> playerUnitsTemplates;
    public List<int> playerUnitsPossibleSpawnCells;

    public List<Unit> enemyUnitsTemplates;
    public List<int> enemyUnitsSpawnCells;
}