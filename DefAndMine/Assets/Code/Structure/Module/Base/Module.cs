using UnityEngine;

public abstract class Module : MonoBehaviour
{
    private ModuleHolder _holder;

    public EDirection Direction => _holder.Direction;
    public StructureBase Structure => _holder.Structure;


    public virtual void Init(ModuleHolder holder)
    {
        _holder = holder;
    }
}