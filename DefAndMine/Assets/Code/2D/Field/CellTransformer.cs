using Cysharp.Threading.Tasks;
using UnityEngine;

public abstract class CellTransformerBase : ScriptableObject
{
    public abstract ECellType TransformType { get; }
    public abstract UniTask TransformAsync(Cell cell);
}