using System.Collections.Generic;

public static class Pathfinder
{
    public static List<Cell> Echo(this Cell cell, int distance)
    {
        var checkedCells = new List<Cell> { cell };
        var echoedCells = new List<Cell>();
        var currentCycleCells = new List<Cell> { cell };
        var nextCycleCells = new List<Cell>();

        for (var i = 0; i < distance; i++)
        {
            currentCycleCells.ForEach(c =>
            {
                CheckAndAddNeighbourCell(c, EDirection.N);
                CheckAndAddNeighbourCell(c, EDirection.E);
                CheckAndAddNeighbourCell(c, EDirection.S);
                CheckAndAddNeighbourCell(c, EDirection.W);
            });

            if (nextCycleCells.Count == 0)
            {
                break;
            }

            currentCycleCells.Clear();
            currentCycleCells.AddRange(nextCycleCells);
            nextCycleCells.Clear();
        }

        return echoedCells;

        void CheckAndAddNeighbourCell(Cell from, EDirection direction)
        {
            var to = from.Neighbour(direction);

            if (to != null && !checkedCells.Contains(to) && to.Unit == null)
            {
                echoedCells.Add(to);
                nextCycleCells.Add(to);
                checkedCells.Add(to);
            }
        }
    }

    public static bool TryBuildPath(this Cell from, Cell to, out List<Cell> path)
    {
        var cellsMoveCost = new int[Field.Width, Field.Length];
        var currentCycleCells = new List<Cell> { from };
        var nextCycleCells = new List<Cell>();

        for (var i = 1; i < Field.Width * Field.Length && cellsMoveCost[to.PosX, to.PosZ] == 0; i++)
        {
            currentCycleCells.ForEach(c =>
            {
                CheckAndAddNeighbourCell(c, EDirection.N, i);
                CheckAndAddNeighbourCell(c, EDirection.E, i);
                CheckAndAddNeighbourCell(c, EDirection.S, i);
                CheckAndAddNeighbourCell(c, EDirection.W, i);
            });

            if (nextCycleCells.Count == 0)
            {
                break;
            }

            currentCycleCells.Clear();
            currentCycleCells.AddRange(nextCycleCells);
            nextCycleCells.Clear();
        }

        if (cellsMoveCost[to.PosX, to.PosZ] == 0)
        {
            path = null;
            return false;
        }

        path = new List<Cell> { to };
        var currentCell = to;

        for (var i = cellsMoveCost[to.PosX, to.PosZ] - 1; i > 0; i--)
        {
            for (var dir = EDirection.N; dir < EDirection.NW; dir += 2)
            {
                if (CheckPathCell(currentCell, dir, i, out var pathCell))
                {
                    currentCell = pathCell;
                    path.Add(pathCell);
                    break;
                }
            }
        }

        path.Reverse();
        return true;

        bool CheckPathCell(Cell cell, EDirection direction, int needMoveCost, out Cell pathCell)
        {
            var neighbour = cell.Neighbour(direction);

            if (neighbour != null && cellsMoveCost[neighbour.PosX, neighbour.PosZ] == needMoveCost)
            {
                pathCell = neighbour;
                return true;
            }

            pathCell = null;
            return false;
        }

        void CheckAndAddNeighbourCell(Cell cell, EDirection direction, int moveCost)
        {
            var neighbour = cell.Neighbour(direction);

            if (neighbour != null && cellsMoveCost[neighbour.PosX, neighbour.PosZ] == 0 && neighbour.Unit == null)
            {
                nextCycleCells.Add(neighbour);
                cellsMoveCost[neighbour.PosX, neighbour.PosZ] = moveCost;
            }
        }
    }
}