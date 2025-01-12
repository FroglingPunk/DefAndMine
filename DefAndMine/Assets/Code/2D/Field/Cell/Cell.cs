using UnityEngine;

public class Cell : ICell
{
    private readonly CellView _view;

    public Unit Unit { get; set; }
    public int PosX { get; private set; }
    public int PosZ { get; private set; }
    public ECellType Type { get; set; }
    public ECellHeight Height { get; set; }
    public ICellContent Content { get; set; }
    public Transform Transform => _view.transform;


    public Cell(CellView view, int posX, int posZ, ECellType type, ECellHeight height, ECellContent contentType,
        EDirection contentDirection)
    {
        _view = view;
        PosX = posX;
        PosZ = posZ;
        Type = type;
        Height = height;

        view.Init(this);

        if (contentType != ECellContent.None)
        {
            var contentTemplate = CellContentStorage.GetContentTemplate(contentType);
            Content = Object.Instantiate(contentTemplate, Transform).GetComponent<ICellContent>();
            Content.Init(this, contentDirection);
        }
    }

    public void SetHighlightState(EHighlightState state)
    {
        _view.SetHighlightState(state);
    }
}