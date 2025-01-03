using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UniRx;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] private CellTransformerBase[] _transformers;
    [SerializeField] private Unit[] _units;
    [SerializeField] private PowerSource _powerSourcePrefab;
    [SerializeField] private PowerTransit _powerTransitPrefab;
    [SerializeField] private PowerLineBuilder _powerLineBuilder;
    
    private readonly Dictionary<Cell, IDisposable> _transformDisposables = new();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            var battleSystem = new BattleTurnSystem(_units);
            battleSystem.StartBattle();
            enabled = false;
        }

        UpdateTransforms();
        UpdatePowerStructures();
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

    private void UpdatePowerStructures()
    {
        var input = Input.GetMouseButtonDown(0) ? 1 :
            Input.GetMouseButtonDown(1) ? 2 : 0;

        if (input == 0 || !TryRaycastCell(out var cell))
        {
            return;
        }

        var mode = Input.GetKey(KeyCode.LeftControl) ? 1 :
            Input.GetKey(KeyCode.LeftAlt) ? 2 : 0;

        if (mode == 0)
        {
            if (cell.Structure == null)
            {
                return;
            }
            
            cell.Structure.Rotate(input == 1);
            _powerLineBuilder.UpdateView();
        }
        else if (mode == 1)
        {
            if (cell.Structure != null)
            {
                return;
            }
            
            var prefab = input == 1 ? (Structure)_powerTransitPrefab : _powerSourcePrefab;
            var structure = Instantiate(prefab, cell.transform);
            structure.Init(cell);
            _powerLineBuilder.UpdateView();
        }
        else if (mode == 2)
        {
            if (cell.Structure == null)
            {
                return;
            }
            
            cell.Structure.Demolish();
            _powerLineBuilder.UpdateView();
        }
    }
    
    private bool TryRaycastCell(out Cell cell)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out var hit))
        {
            if (hit.collider.TryGetComponent<Cell>(out  cell))
            {
                return true;
            }
        }

        cell = null;
        return false;
    }
}