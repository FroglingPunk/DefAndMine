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
    [SerializeField] private ETestFieldPattern _fieldPattern;

    private List<Cell> _cells = new();

    public Cell this[int x, int z] => (x < 0 || z < 0 || x >= Width || z >= Length) ? null : _cells[z * Width + x];



    private void Start()
    {
        CreateField(FieldDataGenerator.Generate(_fieldPattern));
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
            CreateField(FieldDataGenerator.Generate(_fieldPattern));
        }
    }


    private void CreateField(FieldData fieldData)
    {
        for (var z = 0; z < Length; z++)
        {
            for (var x = 0; x < Width; x++)
            {
                var id = (x + z * Width) * 4;

                var type = (ECellType)fieldData.cellsData[id];
                var height = (ECellHeight)fieldData.cellsData[id + 1];
                var content = (ECellContent)fieldData.cellsData[id + 2];
                var contentDirection = (EDirection)fieldData.cellsData[id + 3];

                var cellView = Instantiate(_cellViewPrefab, transform);
                cellView.transform.localPosition = new Vector3(x, 0, z);

                var cell = new Cell(cellView, x, z, type, height, content, contentDirection);
                _cells.Add(cell);
            }
        }
    }


    public bool TryGetFirstContentByDirection(Cell origin, EDirection direction, ECellContent contentType,
        out ICellContent content)
    {
        var delta = direction.ToDeltaField();
        var rayLength = (int)new Vector2(Width, Length).magnitude;

        for (var i = 1; i <= rayLength; i++)
        {
            var nextCell = this[origin.PosX + delta.x * i, origin.PosZ + delta.y * i];

            if (nextCell == null)
            {
                content = default;
                return false;
            }

            if (nextCell.Content != null && nextCell.Content.Type == contentType)
            {
                content = nextCell.Content;
                return true;
            }
        }

        content = default;
        return false;
    }

    public bool TryGetFirstContentByDirectionAs<T>(Cell origin, EDirection direction, ECellContent contentType,
        out T content) where T : IStructure
    {
        var delta = direction.ToDeltaField();
        var rayLength = (int)new Vector2(Width, Length).magnitude;

        for (var i = 1; i <= rayLength; i++)
        {
            var nextCell = this[origin.PosX + delta.x * i, origin.PosZ + delta.y * i];

            if (nextCell == null)
            {
                content = default;
                return false;
            }

            if (nextCell.Content != null && nextCell.Content.Type == contentType)
            {
                content = (T)nextCell.Content;
                return true;
            }
        }

        content = default;
        return false;
    }

    public List<ICellContent> GetContentByType(ECellContent contentType)
    {
        var contents = new List<ICellContent>();

        _cells.ForEach(c =>
        {
            if (c.Content != null && c.Content.Type == contentType)
            {
                contents.Add(c.Content);
            }
        });

        return contents;
    }

    public List<T> GetContentByTypeAs<T>(ECellContent contentType) where T : ICellContent
    {
        var contents = new List<T>();

        _cells.ForEach(c =>
        {
            if (c.Content != null && c.Content.Type == contentType)
            {
                contents.Add((T)c.Content);
            }
        });

        return contents;
    }

    public List<ICellContent> GetContentByPowerTransitType(EPowerTransitType transitType)
    {
        var contents = new List<ICellContent>();

        _cells.ForEach(c =>
        {
            if (c.Content != null && c.Content.PowerTransitType == transitType)
            {
                contents.Add(c.Content);
            }
        });

        return contents;
    }

    public bool TryGetFirstContentByDirectionByPowerTransitType(Cell origin, EDirection direction,
        EPowerTransitType transitType, bool strictTypeChecking,
        out ICellContent content)
    {
        var delta = direction.ToDeltaField();
        var rayLength = (int)new Vector2(Width, Length).magnitude;

        for (var i = 1; i <= rayLength; i++)
        {
            var nextCell = this[origin.PosX + delta.x * i, origin.PosZ + delta.y * i];

            if (nextCell == null)
            {
                content = default;
                return false;
            }

            // если проверка не строгая, то при поиске In или Out вариант InOut считается подходящим
            if (nextCell.Content != null &&
                (nextCell.Content.PowerTransitType == transitType || (!strictTypeChecking &&
                                                                      (transitType == EPowerTransitType.In ||
                                                                       transitType == EPowerTransitType.Out) &&
                                                                      nextCell.Content.PowerTransitType ==
                                                                      EPowerTransitType.InOut)))
            {
                content = nextCell.Content;
                return true;
            }
        }

        content = default;
        return false;
    }

    public void SetCellsHighlightState(EHighlightState state)
    {
        _cells.ForEach(c => c.SetHighlightState(state));
    }

    public List<Cell> SetEchoHighlight(Unit unit, int distance, EHighlightState state)
    {
        var echoCells = unit.Cell.Value.Echo(unit, distance);
        echoCells.ForEach(c => c.SetHighlightState(state));
        return echoCells;
    }
}