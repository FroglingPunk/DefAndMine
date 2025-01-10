using UnityEngine;

public class CellView : MonoBehaviour
{
    public Cell Cell { get; private set; }
    
    // 0 - top, 1 - N, 2 - E, 3 - E, 4 - W, 5 - top highlight
    [SerializeField] private SpriteRenderer[] _spriteRenderers;
    [SerializeField] private Collider _collider;


    public void Init(Cell cell)
    {
        Cell = cell;
        UpdateView(cell.Type, cell.Height);
    }

    public void UpdateView(ECellType type, ECellHeight height)
    {
        var position = transform.localPosition;
        position.y = height switch
        {
            ECellHeight.Elevation => 0.5f,
            ECellHeight.Crevice => 0,
            _ => 0f
        };

        if (type == ECellType.Water)
        {
            position.y -= 0.2f;
        }

        transform.localPosition = position;

        var cellTypeSprites = CellSpritesStorage.GetCellTypeSprites(type);

        _spriteRenderers[0].sprite = cellTypeSprites.topSprite;

        for (var i = 1; i < 5; i++)
        {
            _spriteRenderers[i].color = cellTypeSprites.sideColor;
            // _spriteRenderers[i].sprite = cellTypeSprites.sideSprite;
        }
    }

    public void SetHighlightState(EHighlightState state)
    {
        _spriteRenderers[5].gameObject.SetActive(state != EHighlightState.None);
        
        _spriteRenderers[5].color = state switch
        {
            EHighlightState.ActiveUnit => Color.green,
            EHighlightState.PossibleTarget => Color.white,
            _ => Color.white
        };
    }
}

public enum EHighlightState
{
    None,
    ActiveUnit,
    PossibleTarget
}