using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Object = UnityEngine.Object;

public class DeferredActionView : IDisposable
{
    public readonly DeferredAction action;

    private readonly List<GameObject> _marks;
    private readonly CompositeDisposable _disposables = new();


    public DeferredActionView(DeferredAction action)
    {
        this.action = action;
        action.action.SetupMarksForDeferredAction(action, ref _disposables, ref _marks);
    }

    public void Dispose()
    {
        _disposables?.Dispose();

        for (var i = 0; i < _marks.Count; i++)
        {
            Object.Destroy(_marks[i]);
        }
    }
}