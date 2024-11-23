using Constructor.Structures;
using System.Collections.Generic;
using UnityEngine;
using Constructor.Utils;

public class CellPowerSpreader
{
    static public void Spread()
    {
        bool[,] cellsPower = new bool[Field.Instance.Width, Field.Instance.Length];
        Structure[] structures = Object.FindObjectsOfType<Structure>();
        List<Cell> calcTotalCells = new List<Cell>(Field.Instance.Width * Field.Instance.Length);

        for (int i = 0; i < structures.Length; i++)
        {
            Structure structure = structures[i];

            // у структуры не успевшей заинициализироваться OccupiedCells = null
            AutoInitStructureOnStart structureAutoIniter = null;
            if (structure.TryGetComponent(out structureAutoIniter))
            {
                if (!structureAutoIniter.IsInit)
                {
                    continue;
                }
            }


            Queue<Cell> calcInPrevCycleCells = new Queue<Cell>();

            bool structureNotConnectedToWiringNet = true;
            for (int p = 0; p < structure.Occupied.Length; p++)
            {
                Cell cell = structure.Occupied[p];

                if (cell.Wiring.Value)
                {
                    structureNotConnectedToWiringNet = false;

                    if (!calcTotalCells.Contains(cell))
                    {
                        calcTotalCells.Add(cell);
                        calcInPrevCycleCells.Enqueue(cell);
                    }
                }
            }

            if (structureNotConnectedToWiringNet)
            {
                for (int p = 0; p < structure.Occupied.Length; p++)
                {
                    Cell cell = structure.Occupied[p];

                    cellsPower[cell.XId, cell.ZId] = (structure.PowerReceived >= structure.PowerConsumed);
                }
            }

            while (calcInPrevCycleCells.Count > 0)
            {
                Cell cell = calcInPrevCycleCells.Dequeue();

                for (EDirection dir = EDirection.N; dir < EDirection.NW; dir += 2)
                {
                    Cell neighbor = cell.GetNeighbor(dir);
                    if (neighbor != null && neighbor.Wiring.Value && !calcTotalCells.Contains(neighbor) && !calcInPrevCycleCells.Contains(neighbor))
                    {
                        calcTotalCells.Add(neighbor);
                        calcInPrevCycleCells.Enqueue(neighbor);
                    }
                }
            }
        }

        int totalPowerConsumed = 0;
        int totalPowerReceived = 0;
        for (int p = 0; p < calcTotalCells.Count; p++)
        {
            if (calcTotalCells[p].Structure.Value != null)
            {
                totalPowerReceived += calcTotalCells[p].Structure.Value.PowerReceived;
                totalPowerConsumed += calcTotalCells[p].Structure.Value.PowerConsumed;
            }
        }

        for (int p = 0; p < calcTotalCells.Count; p++)
        {
            cellsPower[calcTotalCells[p].XId, calcTotalCells[p].ZId] = (totalPowerReceived >= totalPowerConsumed);
        }

        for (int z = 0; z < Field.Instance.Length; z++)
        {
            for (int x = 0; x < Field.Instance.Width; x++)
            {
                Field.Instance[x, z].Power.Value = cellsPower[x, z];
            }
        }
    }
}