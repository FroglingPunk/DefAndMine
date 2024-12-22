using UnityEngine;

[System.Serializable]
public class ModuleHolder
{
    [SerializeField] private EDirection _baseDirection;
    [SerializeField] private Transform _transform;

    public Module Module => _transform.GetComponentInChildren<Module>();
    public StructureBase Structure { get; private set; }
    public EDirection Direction => _baseDirection.Plus(Structure.DeltaRotation);

    public void Init(StructureBase structure)
    {
        Structure = structure;
    }
}