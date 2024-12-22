using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class StructureBase : MonoBehaviour
{
    [SerializeField] private List<ModuleHolder> _moduleHolders;
    public List<Module> Modules { get; private set; }
    public Cell Cell { get; private set; }

    public int DeltaRotation { get; private set; }


    public void Init(Cell cell)
    {
        gameObject.SetActive(true);
        transform.position = cell.transform.position;

        Cell = cell;

        Modules = new List<Module>(_moduleHolders.Count);

        for (var i = 0; i < _moduleHolders.Count; i++)
        {
            var holder = _moduleHolders[i];
            holder.Init(this);

            var module = holder.Module;

            if (module != null)
            {
                Modules.Add(module);
                module.Init(holder);
            }
        }

        LocalSceneContainer.MessageBus.Callback(new BuildStructureMessage(this));
    }

    public List<T> GetCertainModules<T>() where T : Module
    {
        return Modules.OfType<T>().ToList();
    }

    public void Rotate(bool clockwise)
    {
        transform.Rotate(new Vector3(0, clockwise ? 45f : -45f, 0));
        DeltaRotation += clockwise ? 1 : -1;
    }

    public void Upgrade()
    {

    }

    public void Destroy()
    {

    }
}