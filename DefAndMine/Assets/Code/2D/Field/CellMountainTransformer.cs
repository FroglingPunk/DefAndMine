using Cysharp.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu(menuName = "Field/Transform/Mountain", fileName = "Mountain Transformer", order = 0)]
public class CellMountainTransformer : CellTransformerBase
{
    [SerializeField] private float _positionY = 0.5f;
    [SerializeField] private Color _color;
    [SerializeField] private float _duration = 1f;
    
    public override ECellType TransformType => ECellType.Mountain;


    public override async UniTask TransformAsync(Cell cell)
    {
        var cellMaterial = cell.Material;

        var startColor = cellMaterial.color;
        var startPosition = cell.transform.localPosition;
        var endPosition = new Vector3(startPosition.x, _positionY, startPosition.z);

        for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime / _duration)
        {
            cellMaterial.color = Color.Lerp(startColor, _color, lerp);
            cell.transform.localPosition = Vector3.Lerp(startPosition, endPosition, lerp);
            await UniTask.Yield();
        }

        cellMaterial.color = _color;
        cell.transform.localPosition = endPosition;
        cell.cellType = TransformType;
    }
}