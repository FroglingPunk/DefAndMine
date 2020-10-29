using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Contstructor
{
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
                direction = direction.Next2();
            }
        }


        public bool Build(Cell fulcrum, EDirection direction, IStructureBuildingData buildingData)
        {
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

                occupiedCells[i] = Field.Instance[fulcrum.XId + directionCorrectionX, fulcrum.ZId + directionCorrectionZ];
            }

            for (int i = 0; i < occupiedCells.Length; i++)
            {
                if (occupiedCells[i] == null || occupiedCells[i].Place != buildingData.Place || occupiedCells[i].Structure.Value != null)
                {
                    Debug.Log(occupiedCells[i].gameObject.name + " NOT EMPTY");
                    return false;
                }

                occupiedCells[i].gameObject.GetComponent<Renderer>().sharedMaterial = occupiedMaterial;
            }

            Structure structure = buildingData.CreateInstance();
            structure.transform.SetParent(transform);
            structure.transform.position = fulcrum.transform.position;
            structure.transform.rotation = Quaternion.Euler(0, 90f * (int)direction / 2, 0);

            structure.Init(occupiedCells);
            return true;
        }

        public Material occupiedMaterial;
    }
}