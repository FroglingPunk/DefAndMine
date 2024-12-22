using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class FieldController : ControllerBase
{
    public int Width { get; private set; }
    public  int Length { get; private set; }

    private List<Cell> _cells = new();
    private Transform _transform;


    public Cell this[int x, int z] => (x < 0 || z < 0 || x >=Width || z >= Length) ? null : _cells[z * Width + x];


    public FieldController(Transform transform)
    {
        _transform = transform;
    }

    public override async UniTask ConstructAsync()
    {
        var prevPosition = _transform.GetChild(0).localPosition;
        var minPosY = prevPosition.y;
        var maxPosY = prevPosition.y;

        for (var i = 1; i < _transform.childCount; i++)
        {
            var currentPosition = _transform.GetChild(i).localPosition;

            if (currentPosition.y < minPosY)
            {
                minPosY = currentPosition.y;
            }
            else if (currentPosition.y > maxPosY)
            {
                maxPosY = currentPosition.y;
            }

            if (Width == 0 && Mathf.Abs(currentPosition.z - prevPosition.z) >= 0.5f)
            {
                Width = i;
                Length = _transform.childCount / Width;
                break;
            }
        }

        for (var i = 0; i < _transform.childCount; i++)
        {
            var cellTransform = _transform.GetChild(i);
            var position = cellTransform.localPosition;

            var height = maxPosY - minPosY < 0.1f ? ECellHeight.Ground :
                position.y - minPosY < maxPosY - position.y ? ECellHeight.Ground : ECellHeight.Elevation;

            var cell = cellTransform.GetComponent<Cell>();
            cell.Init(i % Width, i / Length, height);
            _cells.Add(cell);
        }

        await base.InitAsync();
    }



    public bool TryGetFirstStructureByDirection(Cell origin, EDirection direction, out StructureBase structure)
    {
        var delta = direction.ToDeltaField();
        var rayLength = (int)new Vector2(Width, Length).magnitude;

        for (var i = 0; i < rayLength; i++)
        {
            var nextCell = this[origin.X + delta.x, origin.Z + delta.y];

            if (nextCell == null)
            {
                structure = null;
                return false;
            }

            if (nextCell.Structure.Value != null)
            {
                structure = nextCell.Structure.Value;
                return true;
            }
        }

        structure = null;
        return false;
    }
}