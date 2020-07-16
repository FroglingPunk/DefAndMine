using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrucuresBuilder : MonoBehaviour
{
    [SerializeField]
    private StructureBuildingData structureBuildingData;

    [SerializeField]
    private EDirection direction = EDirection.N;


    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Cell cell;
                if (hit.collider.TryGetComponent(out cell))
                {
                    Build(cell, direction, structureBuildingData);
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            direction = direction.Next();
        }
    }


    public void Build(Cell fulcrum, EDirection direction, StructureBuildingData buildingData)
    {
        Structure structure = Instantiate
            (
                buildingData.Prefab,
                fulcrum.transform.position,
                Quaternion.Euler(0, 90f * (int)direction, 0),
                transform
            );

        Vector2Int[] occupiedCellsDelta = buildingData.Occupied;
        Cell[] occupiedCells = new Cell[occupiedCellsDelta.Length];

        for (int i = 0; i < occupiedCellsDelta.Length; i++)
        {
            int directionCorrectionX = occupiedCellsDelta[i].x;
            int directionCorrectionZ = occupiedCellsDelta[i].y;

            if (direction == EDirection.E)
            {
                directionCorrectionX = occupiedCellsDelta[i].y;
                directionCorrectionZ = -occupiedCellsDelta[i].x;
            }
            else if (direction == EDirection.S)
            {
                directionCorrectionX = -occupiedCellsDelta[i].x;
                directionCorrectionZ = -occupiedCellsDelta[i].y;
            }
            else if (direction == EDirection.W)
            {
                directionCorrectionX = -occupiedCellsDelta[i].y;
                directionCorrectionZ = occupiedCellsDelta[i].x;
            }

            occupiedCells[i] = fulcrum.Field[fulcrum.IDx + directionCorrectionX, fulcrum.IDz + directionCorrectionZ];
        }

        structure.Init(occupiedCells);
    }
}