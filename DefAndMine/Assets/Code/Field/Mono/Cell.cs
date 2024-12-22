using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] private BoxCollider _collider;
    [SerializeField] private HighlighterBase _highlighter;

    public int X { get; private set; }
    public int Z { get; private set; }
    public ECellHeight Height { get; private set; }

    public IReadOnlyReactiveProperty<StructureBase> Structure => _structure;

    private readonly ReactiveProperty<StructureBase> _structure = new();

    private readonly CompositeDisposable _disposables = new();


    public void Init(int x, int z, ECellHeight height)
    {
        X = x;
        Z = z;
        Height = height;

        LocalSceneContainer.MessageBus.Subscribe<BuildStructureMessage>(s =>
        {
            if (s.Structure.Cell == this)
            {
                _structure.Value = s.Structure;
            }
        });
        
        // _collider.OnMouseEnterAsObservable().Subscribe((_) => _highlighter.SetState(true)).AddTo(_disposables);
        //
        // _collider.OnMouseExitAsObservable().Subscribe((_) => _highlighter.SetState(false)).AddTo(_disposables);

        _collider.OnMouseDownAsObservable()
            .Subscribe((_) => LocalSceneContainer.MessageBus.Callback(new SelectMessage<Cell>(this)))
            .AddTo(_disposables);
    }
}