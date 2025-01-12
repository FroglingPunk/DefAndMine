using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerLineBuilder : MonoBehaviour
{
    [SerializeField] private LineRenderer _linePrefab;
    [SerializeField] private Field _field;

    private Queue<LineRenderer> _linesPool = new();
    private List<LineRenderer> _activeLines = new();


    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0.2f);
        UpdateView();
    }

    public void UpdateView()
    {
        Clear();

        var sources = _field.GetContentByPowerTransitType(EPowerTransitType.Out);

        for (var i = 0; i < sources.Count; i++)
        {
            var source = sources[i];
            var rayCells = new List<Cell> { source.Cell };

            var prevCell = source.Cell;
            var prevDir = source.Direction;

            while (true)
            {
                if (!_field.TryGetFirstContentByDirectionByPowerTransitType(prevCell, prevDir, EPowerTransitType.In,
                        false, out var powerConsumer))
                {
                    break;
                }

                if (rayCells.Contains(powerConsumer.Cell))
                {
                    break;
                }

                rayCells.Add(powerConsumer.Cell);

                if (powerConsumer.PowerTransitType == EPowerTransitType.InOut)
                {
                    prevCell = powerConsumer.Cell;
                    prevDir = powerConsumer.Direction;
                }
            }

            if (rayCells.Count == 1)
            {
                continue;
            }

            var line = GetLine();
            var linePoints = new List<Vector3>();
            rayCells.ForEach(rc => linePoints.Add(rc.Transform.position));
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