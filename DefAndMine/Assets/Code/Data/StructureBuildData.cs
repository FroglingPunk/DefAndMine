using UnityEngine;

[CreateAssetMenu(menuName = "Data/Structure", fileName = "Structure Build Data", order = 0)]
public class StructureData : ScriptableObject
{
    [field: SerializeField] public string Name { get; private set; }
    [field: SerializeField] public StructureBase Prefab { get; private set; }
}