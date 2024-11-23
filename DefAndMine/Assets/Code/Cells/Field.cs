using System;
using UnityEngine;

public class Field : MonoBehaviour
{
    static private Field instance;
    static public Field Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Field>();
            }

            return instance;
        }
    }

    // "кнопка" для заполнения данных о плоскости на основе клеток в transform.childrens
    [SerializeField] private bool VALIDATE_COLLECT_CELLS = false;
    [SerializeField] private Cell[] cells;

    // ширина плоскости (клетки вдоль оси X)
    [SerializeField] private int width;
    // длина плоскости (клетки вдоль оси Z)
    [SerializeField] private int length;

    public int Width => width;
    public int Length => length;

    public Cell this[int x, int z]
    {
        get
        {
            return (x < 0 || z < 0 || x >= width || z >= length) ? null : cells[z * width + x];
        }
    }

    public Material defaultCellMaterial;
    public Material chosenCellMaterial;


    void OnValidate()
    {
        if (VALIDATE_COLLECT_CELLS)
        {
            VALIDATE_COLLECT_CELLS = false;

            cells = GetComponentsInChildren<Cell>();

            for (int i = 1; i < cells.Length; i++)
            {
                if (cells[i].transform.localPosition.z != cells[i - 1].transform.localPosition.z)
                {
                    width = i;
                    length = cells.Length / width;
                    break;
                }
            }

            for (int z = 0; z < length; z++)
            {
                for (int x = 0; x < width; x++)
                {
                    cells[x + z * width].name = "Cell [" + x + ":" + z + "]";
                }
            }
        }
    }

    void Awake()
    {
        Control.Instance.OnCursorCellChanged += OnCursorCellChanged;
        Control.Instance.OnCellChosen += SwitchWiring;

        Init();
    }

    
    private void OnCursorCellChanged(Cell previousCell, Cell currentCell)
    {
        if (previousCell)
        {
            previousCell.GetComponent<Renderer>().sharedMaterial = defaultCellMaterial;
        }
        if (currentCell)
        {
            currentCell.GetComponent<Renderer>().sharedMaterial = chosenCellMaterial;
        }
    }

    private void SwitchWiring(Cell cell)
    {
        cell.Wiring.Value = !cell.Wiring.Value;
    }


    public void Init()
    {
        for (int z = 0; z < Length; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                cells[x + z * Width].Init(x, z);
            }
        }
    }

    public void ActionAllCells(Action<Cell> action)
    {
        for (int z = 0; z < Length; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                action(cells[z * width + x]);
            }
        }
    }
}