using UnityEngine;

[System.Serializable]
public class CellTypeSprite
{
    public ECellType type;

    public Sprite[] topSprite;
    public Sprite sideSprite;

    public Color topColor;
    public Color sideColor;
}