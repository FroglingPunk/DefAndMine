using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UniRx;
using Object = UnityEngine.Object;

public class StructureBuildPanel : UIControllerModel<StructureBuildPanelView>
{
    private ControlPanel _controlPanel;
    private List<StructureBuildElement> _elements = new();
    private CancellationTokenSource _buildCslTokenSource;


    public StructureBuildPanel(StructureBuildPanelView view) : base(view)
    {
    }

    public override async UniTask ConstructAsync()
    {
        _controlPanel = LocalSceneContainer.GetController<ControlPanel>();
        
        for (var i = 0; i < _view.StructuresData.Length; i++)
        {
            _elements.Add(
                new StructureBuildElement(_view.ElementTemplate, _view.ScrollRect.content, _view.StructuresData[i],
                    OnElementClick));
        }

        _view.ButtonBack.onClick.AddListener(OnButtonBackClick);

        await base.ConstructAsync();
    }

    private void OnButtonBackClick()
    {
        if (_buildCslTokenSource == null)
        {
            SetVisibilityState(false);
            _controlPanel.SetVisibilityState(true);
        }
        else
        {
            _buildCslTokenSource.Cancel();
            _buildCslTokenSource.Dispose();
            _buildCslTokenSource = null;
        
            SetElementsVisibility(true);
        }
    }

    private void OnElementClick(StructureData structureData)
    {
        SetElementsVisibility(false);
        
        _buildCslTokenSource = new CancellationTokenSource();
        BuildStructureAsync(structureData, _buildCslTokenSource.Token).Forget();
    }

    private async UniTask BuildStructureAsync(StructureData structureData, CancellationToken cslToken)
    {
        var raycastPointer = new RaycastPointer<Cell>();
        var structure = Object.Instantiate(structureData.Prefab);
        structure.gameObject.SetActive(false);

        raycastPointer.OnEnter.Subscribe(cell =>
        {
            structure.gameObject.SetActive(true);
            structure.transform.position = cell.transform.position;
        });

        raycastPointer.OnExit.Subscribe(cell => { structure.gameObject.SetActive(false); });

        var buildCellSelected = false;

        raycastPointer.OnClick.Subscribe(cell =>
        {
            structure.Init(cell);
            buildCellSelected = true;
        });

        while (!buildCellSelected && !cslToken.IsCancellationRequested)
        {
            await UniTask.Yield();
        }

        if (cslToken.IsCancellationRequested)
        {
            Object.Destroy(structure.gameObject);
        }
        else
        {
            _buildCslTokenSource = null;
        }

        raycastPointer.Dispose();
        SetElementsVisibility(true);
    }


    private void SetElementsVisibility(bool state)
    {
        var elementsParent = _view.ScrollRect.content;

        for (var i = 1; i < elementsParent.childCount; i++)
        {
            elementsParent.GetChild(i).gameObject.SetActive(state);
        }
    }
}