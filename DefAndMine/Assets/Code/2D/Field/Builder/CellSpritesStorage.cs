using System.Collections.Generic;
using UnityEngine;

public class CellSpritesStorage : MonoBehaviour
{
    public static CellSpritesStorage Instance { get; private set; }

    [SerializeField] private List<CellTypeSprite> _typeSprites;

    [field: SerializeField] public GameObject MarkSourceTemplate { get; private set; }
    [field: SerializeField] public GameObject MarkTargetTemplate { get; private set; }

    
    private void Awake()
    {
        Instance = this;
    }

    public static CellTypeSprite GetCellTypeSprites(ECellType type)
    {
        return Instance._typeSprites.Find(s => s.type == type);
    }
}