using System;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

public class RaycastPointer<T> : IDisposable
{
    public IReadOnlyReactiveProperty<T> OnEnter { get; private set; }
    public IReadOnlyReactiveProperty<T> OnExit { get; private set; }
    public IReadOnlyReactiveProperty<T> OnClick { get; private set; }

    private ReactiveCommand<T> _onEnter = new();
    private ReactiveCommand<T> _onExit = new();
    private ReactiveCommand<T> _onClick = new();

    private CompositeDisposable _disposables = new();
    private Camera _camera;
    private T _hoveredRayResult;


    public RaycastPointer()
    {
        _camera = Camera.main;

        OnEnter = _onEnter.ToReadOnlyReactiveProperty().AddTo(_disposables);
        OnExit = _onExit.ToReadOnlyReactiveProperty().AddTo(_disposables);
        OnClick = _onClick.ToReadOnlyReactiveProperty().AddTo(_disposables);

        Observable.EveryUpdate().Subscribe((_) => Update()).AddTo(_disposables);
    }

    public void Dispose()
    {
        _disposables?.Dispose();
    }

    private void Update()
    {
        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        
        if (!EventSystem.current.IsPointerOverGameObject() && 
            Physics.Raycast(ray, out var hit) && hit.transform.TryGetComponent<T>(out var rayResult))
        {
            if (_hoveredRayResult == null)
            {
                _hoveredRayResult = rayResult;
                _onEnter?.Execute(rayResult);
            }
            else if (!_hoveredRayResult.Equals(rayResult))
            {
                _onExit?.Execute(_hoveredRayResult);
                _hoveredRayResult = rayResult;
                _onEnter?.Execute(rayResult);
            }

            if (_hoveredRayResult != null && Input.GetMouseButtonDown(0))
            {
                _onClick?.Execute(rayResult);
            }
        }
        else
        {
            if (_hoveredRayResult != null)
            {
                _onExit?.Execute(_hoveredRayResult);
                _hoveredRayResult = default;
            }
        }
    }
}