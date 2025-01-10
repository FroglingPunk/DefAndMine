using System;
using Random = UnityEngine.Random;

public class FieldDataGenerator
{
    public static FieldData Generate()
    {
        var cellsData = new int[Field.Width * Field.Length * 2];

        for (var z = 0; z < Field.Length; z++)
        {
            for (var x = 0; x < Field.Width; x++)
            {
                var id = (x + z * Field.Width) * 2;
                
                cellsData[id] = Random.Range(0, Enum.GetValues(typeof(ECellType)).Length);
                cellsData[id + 1] = Random.Range(0, Enum.GetValues(typeof(ECellHeight)).Length);
            }
        }

        return new FieldData { cellsData = cellsData };
    }
    
    
    public static FieldData GetClearField()
    {
        var cellsData = new int[Field.Width * Field.Length * 2];

        for (var z = 0; z < Field.Length; z++)
        {
            for (var x = 0; x < Field.Width; x++)
            {
                cellsData[x + z * Field.Width] = (int)ECellType.Grass;
                cellsData[x + z * Field.Width + 1] = (int)ECellHeight.Ground;
            }
        }

        return new FieldData { cellsData = cellsData };
    }
}