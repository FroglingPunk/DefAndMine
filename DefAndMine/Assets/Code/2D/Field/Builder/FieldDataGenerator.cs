using System;
using Random = UnityEngine.Random;

public class FieldDataGenerator
{
    private static FieldData GenerateRandom()
    {
        var cellsData = new int[Field.Width * Field.Length * 2];

        for (var z = 0; z < Field.Length; z++)
        {
            for (var x = 0; x < Field.Width; x++)
            {
                var id = (x + z * Field.Width) * 4;

                cellsData[id] = Random.Range(0, Enum.GetValues(typeof(ECellType)).Length);
                cellsData[id + 1] = Random.Range(0, Enum.GetValues(typeof(ECellHeight)).Length);
                cellsData[id + 2] = (int)ECellContent.None;
                cellsData[id + 3] = (int)EDirection.N;
            }
        }

        return new FieldData { cellsData = cellsData };
    }

    private static FieldData GenerateClear()
    {
        var cellsData = new int[Field.Width * Field.Length * 2];

        for (var z = 0; z < Field.Length; z++)
        {
            for (var x = 0; x < Field.Width; x++)
            {
                var id = (x + z * Field.Width) * 4;
                
                cellsData[id] = (int)ECellType.Grass;
                cellsData[id + 1] = (int)ECellHeight.Ground;
                cellsData[id + 2] = (int)ECellContent.None;
                cellsData[id + 3] = (int)EDirection.N;
            }
        }

        return new FieldData { cellsData = cellsData };
    }

    private static FieldData GenerateWaterGrassRows()
    {
        var cellsData = new int[Field.Width * Field.Length * 2];

        for (var z = 0; z < Field.Length; z++)
        {
            for (var x = 0; x < Field.Width; x++)
            {
                var id = (x + z * Field.Width) * 4;

                cellsData[id] = z % 2 == 0 ? (int)ECellType.Grass : (int)ECellType.Water;
                cellsData[id + 1] = (int)ECellHeight.Ground;
                cellsData[id + 2] = (int)ECellContent.None;
                cellsData[id + 3] = (int)EDirection.N;
            }
        }

        return new FieldData { cellsData = cellsData };
    }

    private static FieldData GenerateDemoTowerFailure_0()
    {
        var cellsData = new int[]
        {
            // row 0
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.Mountain, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.Mountain, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.PlaceOfConstruction, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.PlaceOfConstruction, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            // row 1
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            // row 2
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.Mountain, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Elevation, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Elevation, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Elevation, (int)ECellContent.PowerTransit, (int)EDirection.E,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.RobotFactory, (int)EDirection.N,
            // row 3
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Elevation, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            // row 4
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.Mountain, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.PlaceOfConstruction, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Elevation, (int)ECellContent.PowerSource, (int)EDirection.S,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            // row 5
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.RocketTower, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            // row 6
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.Bridge, (int)EDirection.E,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            // row 7
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.PowerSource, (int)EDirection.E,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.PowerTransit, (int)EDirection.S,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.PlaceOfConstruction, (int)EDirection.N,
            (int)ECellType.Water, (int)ECellHeight.Ground, (int)ECellContent.None, (int)EDirection.N,
            (int)ECellType.MarsSoil, (int)ECellHeight.Ground, (int)ECellContent.PowerSource, (int)EDirection.E,
            (int)ECellType.Asphalt, (int)ECellHeight.Ground, (int)ECellContent.RobotFactory, (int)EDirection.N
        };

        return new FieldData { cellsData = cellsData };
    }


    public static FieldData Generate(ETestFieldPattern pattern)
    {
        return pattern switch
        {
            ETestFieldPattern.ClearField => GenerateClear(),
            ETestFieldPattern.Random => GenerateRandom(),
            ETestFieldPattern.WaterGrassRows => GenerateWaterGrassRows(),
            ETestFieldPattern.Demo_TowerFailure_0 => GenerateDemoTowerFailure_0(),
            
            _ => GenerateClear()
        };
    }
}

public enum ETestFieldPattern
{
    ClearField,
    Random,
    WaterGrassRows,
    Demo_TowerFailure_0
}