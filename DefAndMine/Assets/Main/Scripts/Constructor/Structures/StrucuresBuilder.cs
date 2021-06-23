using System.IO;
using UnityEngine;

namespace Constructor.Structures
{
    public class StrucuresBuilder : MonoBehaviour
    {
        private static Transform structuresTransformParent;
        private static Transform StructuresTransformParent
        {
            get
            {
                if(structuresTransformParent == null)
                {
                    structuresTransformParent = new GameObject("[STRUCTURES]").transform;
                }

                return structuresTransformParent;
            }
        }

        [SerializeField] private StructuresPanel uiStructuresPanel;
        [SerializeField] private EDirection direction = EDirection.N;


        void Awake()
        {
            Control.Instance.OnCellChosen += TryBuild;
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                direction = direction.Next2();
            }
        }


        private void TryBuild(Cell cell)
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "structureBuildingData.txt"));
                SerializableStructureBuildingData buildingData = JsonUtility.FromJson<SerializableStructureBuildingData>(json);

                if (Polly.Instance.ResourcesStorage.CanAfford(buildingData.Cost.All))
                {
                    if (Build(cell, direction, buildingData, "Player"))
                    {
                        Polly.Instance.ResourcesStorage.TryDecrease(buildingData.Cost.All);
                    }
                }
            }
            else
            {
                StructureBuildingData buildingData = uiStructuresPanel.PickedStructureBuildingData;
                if (buildingData != null)
                {
                    if (Polly.Instance.ResourcesStorage.CanAfford(buildingData.Cost.All))
                    {
                        if (Build(cell, direction, buildingData, "Player"))
                        {
                            Polly.Instance.ResourcesStorage.TryDecrease(buildingData.Cost.All);
                        }
                    }
                }
            }
        }

        public static bool Build(Cell fulcrum, EDirection direction, IStructureBuildingData buildingData, string team)
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
            }

            Structure structure = buildingData.CreateInstance();
            structure.transform.SetParent(StructuresTransformParent);
            structure.transform.position = fulcrum.transform.position;
            structure.transform.rotation = Quaternion.Euler(0, 90f * (int)direction / 2, 0);

            structure.Init(occupiedCells, team);
            return true;
        }
    }
}