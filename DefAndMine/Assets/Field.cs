using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field : MonoBehaviour
{
    [SerializeField]
    private Cell cell_prefab;
    [SerializeField]
    private Transform cells_parent;

    [SerializeField]
    private int width;
    [SerializeField]
    private int length;

    private Cell[,] cells;

    public Cell this[int x, int z] => cells[x, z];

    public int Width => width;
    public int Length => length;



    void Start()
    {
        cells = new Cell[Width, Length];

        for (int z = 0; z < Length; z++)
        {
            for (int x = 0; x < Width; x++)
            {
                cells[x, z] = Instantiate(cell_prefab, new Vector3(-Width / 2f + x + 0.5f, 0, -Length / 2f + z + 0.5f), Quaternion.identity, cells_parent);
                cells[x, z].Init(this, x, z);

                if (x > 0)
                {
                    cells[x, z].SetNeighbor(cells[x - 1, z], EDirection.W);
                }

                if (z > 0)
                {
                    cells[x, z].SetNeighbor(cells[x, z - 1], EDirection.S);
                }
            }
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Cell cell;
                if (hit.collider.TryGetComponent(out cell))
                {
                    cell.Wiring.Value = !cell.Wiring.Value;
                }
            }
        }
    }
}