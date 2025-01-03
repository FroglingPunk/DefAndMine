using System.Collections.Generic;
using UnityEngine;

public class PowerLineBuilder : MonoBehaviour
{
    [SerializeField] private LineRenderer _linePrefab;
    [SerializeField] private Field _field;

    private Queue<LineRenderer> _linesPool = new();
    private List<LineRenderer> _activeLines = new();


    public void UpdateView()
    {
        Clear();

        var sources = _field.GetStructuresByType<PowerSource>();
        
        for (var i = 0; i < sources.Count; i++)
        {
            var source = sources[i];
            var rayCells = new List<Cell> { source.Cell };

            var prevCell = source.Cell;
            var prevDir = source.Direction;

            while (true)
            {
                if (!_field.TryGetFirstStructureByDirection<PowerTransit>(prevCell, prevDir, out var transit))
                {
                    break;
                }

                if (rayCells.Contains(transit.Cell))
                {
                    break;
                }

                rayCells.Add(transit.Cell);
                prevCell = transit.Cell;
                prevDir = transit.Direction;
            }

            if (rayCells.Count == 1)
            {
                continue;
            }

            var line = GetLine();
            var linePoints = new List<Vector3>();
            rayCells.ForEach(rc => linePoints.Add(rc.transform.position));
            line.positionCount = linePoints.Count;
            line.SetPositions(linePoints.ToArray());
        }
    }

    private void Clear()
    {
        for (var i = 0; i < _activeLines.Count; i++)
        {
            ReleaseLine(_activeLines[i]);
        }

        _activeLines.Clear();
    }


    private void ReleaseLine(LineRenderer line)
    {
        line.gameObject.SetActive(false);
        _linesPool.Enqueue(line);
    }

    private LineRenderer GetLine()
    {
        var line = _linesPool.Count > 0 ? _linesPool.Dequeue() : Instantiate(_linePrefab, transform);
        line.gameObject.SetActive(true);
        _activeLines.Add(line);
        return line;
    }
}