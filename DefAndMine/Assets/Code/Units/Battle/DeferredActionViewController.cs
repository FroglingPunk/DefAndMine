using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class DeferredActionViewController : MonoBehaviour
{
    private List<DeferredActionView> _actionViews = new();
    private CompositeDisposable _disposables = new();


    private void Start()
    {
        _disposables.Add(BattleTurnSystem.Instance.DeferredActionsList.Actions.ObserveAdd()
            .Subscribe(x => AddView(x.Value)));

        _disposables.Add(BattleTurnSystem.Instance.DeferredActionsList.Actions.ObserveRemove()
            .Subscribe(x => RemoveView(x.Value)));
    }

    private void OnDestroy()
    {
        _disposables?.Dispose();
    }


    private void AddView(DeferredAction action)
    {
        _actionViews.Add(new(action));
    }

    private void RemoveView(DeferredAction action)
    {
        var view = _actionViews.Find(a => a.action == action);
        _actionViews.Remove(view);
        view?.Dispose();
    }
}