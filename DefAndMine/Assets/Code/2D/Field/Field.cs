using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class Field : MonoBehaviour
{
    public static Field Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<Field>();
            }

            return _instance;
        }
    }

    private static Field _instance;
    
    [SerializeField] private float _rotationTime = 1f;
    [SerializeField] private Transform _cellsParent;
    
    public int Width { get; private set; }
    public int Length { get; private set; }

    private List<Cell> _cells = new();
    private bool _isRotatingNow;

    public Cell this[int x, int z] => (x < 0 || z < 0 || x >= Width || z >= Length) ? null : _cells[z * Width + x];


    private void Start()
    {
        var prevPosition = _cellsParent.GetChild(0).localPosition;

        for (var i = 1; i < _cellsParent.childCount; i++)
        {
            var currentPosition = _cellsParent.GetChild(i).localPosition;

            if (Width == 0 && Mathf.Abs(currentPosition.z - prevPosition.z) >= 0.5f)
            {
                Width = i;
                Length = _cellsParent.childCount / Width;
                break;
            }
        }

        for (var i = 0; i < _cellsParent.childCount; i++)
        {
            var cellTransform = _cellsParent.GetChild(i);

            var cell = cellTransform.GetComponent<Cell>();
            cell.Init(i % Width, i / Length);
            _cells.Add(cell);
        }
    }

    public void Rotate(bool clockwise)
    {
        if (_isRotatingNow)
        {
            return;
        }

        RotateAsync(clockwise).Forget();
    }

    public bool TryGetFirstStructureByDirection<T>(Cell origin, EDirection direction, out T structure)
        where T : IStructure
    {
        var delta = direction.ToDeltaField();
        var rayLength = (int)new Vector2(Width, Length).magnitude;

        for (var i = 1; i <= rayLength; i++)
        {
            var nextCell = this[origin.X + delta.x * i, origin.Z + delta.y * i];

            if (nextCell == null)
            {
                structure = default;
                return false;
            }

            if (nextCell.Structure is T)
            {
                structure = (T)nextCell.Structure;
                return true;
            }
        }

        structure = default;
        return false;
    }

    public List<T> GetStructuresByType<T>() where T : IStructure
    {
        var structures = new List<T>();

        _cells.ForEach(c =>
        {
            if (c.Structure is T)
            {
                structures.Add((T)c.Structure);
            }
        });

        return structures;
    }

    public void SetCellsHighlightState(bool state)
    {
        _cells.ForEach(c => c.SetHighlightState(state));
    }

    public List<Cell> SetEchoHighlight(Cell cell, int distance, bool state)
    {
        var echoCells = cell.Echo(distance);
        echoCells.ForEach(c => c.SetHighlightState(state));
        return echoCells;
    }
    
    private async UniTask RotateAsync(bool clockwise)
    {
        _isRotatingNow = true;

        var startEuler = transform.localEulerAngles;
        var endEuler = startEuler + new Vector3(0, clockwise ? 90f : -90f, 0f);

        for (var lerp = 0f; lerp < 1f; lerp += Time.deltaTime / _rotationTime)
        {
            transform.localEulerAngles = Vector3.Lerp(startEuler, endEuler, lerp);
            await UniTask.Yield();
        }

        transform.localEulerAngles = endEuler;

        _isRotatingNow = false;
    }
}