using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private GameObject _highlightGo;
    
    private Material _material;

    public Material Material
    {
        get
        {
            if (_material == null)
            {
                _material = GetComponent<Renderer>().material;
            }

            return _material;
        }
    }

    public int X { get; private set; }
    public int Z { get; private set; }
    public Unit Unit { get; set; }
    public IStructure Structure { get; private set; }

    public ECellType cellType = ECellType.Grass;

    public void Init(int x, int z)
    {
        X = x;
        Z = z;

        // LocalSceneContainer.MessageBus.Subscribe<BuildStructureMessage>(s =>
        // {
        //     if (s.Structure.Cell == this)
        //     {
        //         _structure.Value = s.Structure;
        //     }
        // });

        // _collider.OnMouseEnterAsObservable().Subscribe((_) => _highlighter.SetState(true)).AddTo(_disposables);
        //
        // _collider.OnMouseExitAsObservable().Subscribe((_) => _highlighter.SetState(false)).AddTo(_disposables);

        // _collider.OnMouseDownAsObservable()
        //     .Subscribe((_) => LocalSceneContainer.MessageBus.Callback(new SelectMessage<Cell>(this)))
        //     .AddTo(_disposables);
    }

    public void SetStructure(IStructure structure)
    {
        Structure = structure;
    }

    public void SetHighlightState(bool state)
    {
        _highlightGo.SetActive(state);
    }
}

public static class CellExtensions
{
    public static Cell Neighbour(this Cell cell, EDirection direction)
    {
        var delta = direction.ToDeltaField();
        return Field.Instance[cell.X + delta.x, cell.Z + delta.y];
    }
}