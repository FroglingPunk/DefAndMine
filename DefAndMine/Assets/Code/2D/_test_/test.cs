using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private BattleData _battleData;
    [SerializeField] private CellTransformerBase[] _transformers;
    
    private readonly Dictionary<Cell, IDisposable> _transformDisposables = new();
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            BattleTurnSystem.Instance.StartBattle(_battleData);
            enabled = false;
        }

        UpdateTransforms();
    }


    private void UpdateTransforms()
    {
        var input = Input.GetKeyDown(KeyCode.Alpha1) ? 0 :
            Input.GetKeyDown(KeyCode.Alpha2) ? 1 :
            Input.GetKeyDown(KeyCode.Alpha3) ? 2 :
            -1;

        if (input == -1 || !TryRaycastCell(out var cell) || _transformDisposables.ContainsKey(cell))
        {
            return;
        }

        _transformDisposables.Add(cell,
            _transformers[input].TransformAsync(cell).ToObservable().Subscribe(_ =>
            {
                _transformDisposables[cell]?.Dispose();
                _transformDisposables.Remove(cell);
            }));
    }
    
    private bool TryRaycastCell(out Cell cell)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent<CellView>(out var cellView))
            {
                cell = cellView.Cell;
                return true;
            }
        }

        cell = null;
        return false;
    }
}