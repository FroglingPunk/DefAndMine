using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    public const int Width = 8;
    public const int Length = 8;
    
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
    
    [SerializeField] private CellView _cellViewPrefab;
    [SerializeField] private bool _debugUseRandomField;
    
    private List<Cell> _cells = new();

    public Cell this[int x, int z] => (x < 0 || z < 0 || x >= Width || z >= Length) ? null : _cells[z * Width + x];



    private void Start()
    {
        var fieldData = _debugUseRandomField ? FieldDataGenerator.Generate() : FieldDataGenerator.GetClearField();
        CreateField(fieldData);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (var i = transform.childCount - 1; i >= 0; i--)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            
            _cells.Clear();

            var fieldData = _debugUseRandomField ? FieldDataGenerator.Generate() : FieldDataGenerator.GetClearField();
            CreateField(fieldData);
        }
    }


    private void CreateField(FieldData fieldData)
    {
        for (var z = 0; z < Length; z++)
        {
            for (var x = 0; x < Width; x++)
            {
                var id = (x + z * Width) * 2;
                
                var type = (ECellType)fieldData.cellsData[id];
                var height = (ECellHeight)fieldData.cellsData[id + 1];
                var cellView = Instantiate(_cellViewPrefab, transform);
                cellView.transform.localPosition = new Vector3(x, 0, z);

                var cell = new Cell(cellView, x, z, type, height);
                _cells.Add(cell);
            }
        }
    }
    

    public bool TryGetFirstStructureByDirection<T>(Cell origin, EDirection direction, out T structure)
        where T : IStructure
    {
        var delta = direction.ToDeltaField();
        var rayLength = (int)new Vector2(Width, Length).magnitude;

        for (var i = 1; i <= rayLength; i++)
        {
            var nextCell = this[origin.PosX + delta.x * i, origin.PosZ + delta.y * i];

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

    public void SetCellsHighlightState(EHighlightState state)
    {
        _cells.ForEach(c => c.SetHighlightState(state));
    }

    public List<Cell> SetEchoHighlight(Cell cell, int distance, EHighlightState state)
    {
        var echoCells = cell.Echo(distance);
        echoCells.ForEach(c => c.SetHighlightState(state));
        return echoCells;
    }
}