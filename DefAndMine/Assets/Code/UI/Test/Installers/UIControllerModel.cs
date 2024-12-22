using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public interface IUIControllerModel : IController
{

}

public abstract class UIControllerModel<T> : IUIControllerModel where T : UIControllerView
{
    protected readonly T _view;

    protected UIControllerModel(T view)
    {
        _view = view;
    }


    public virtual void SetVisibilityState(bool value)
    {
        _view.gameObject.SetActive(value);
    }


    public virtual async UniTask ConstructAsync()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask InitAsync()
    {
        await UniTask.Yield();
    }

    public virtual async UniTask DeinitAsync()
    {
        await UniTask.Yield();
    }
}