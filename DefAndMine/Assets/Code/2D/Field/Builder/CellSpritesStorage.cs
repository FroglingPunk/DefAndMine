using System.Collections.Generic;
using UnityEngine;

public class CellSpritesStorage : MonoBehaviour
{
    [SerializeField] private List<CellTypeSprite> _typeSprites;

    private static CellSpritesStorage _instance;


    private void Awake()
    {
        _instance = this;
    }


    public static CellTypeSprite GetCellTypeSprites(ECellType type)
    {
        return _instance._typeSprites.Find(s => s.type == type);
    }
}