using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Constructor.Structures
{
    public class StructuresContstructor : MonoBehaviour
    {
        [SerializeField] private ConstructCell[] cells;
        [SerializeField] private BlockBuildingData[] possibleBlocks;
        [SerializeField] private ModuleBuildingData[] possibleModules;


        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    ConstructCell constructCell = null;
                    if (hit.collider.TryGetComponent(out constructCell))
                    {
                        constructCell.Block = Instantiate(possibleBlocks[0].Prefab);
                    }

                    BlockModulePlace blockModulePlace = null;
                    if (hit.collider.TryGetComponent(out blockModulePlace))
                    {
                        blockModulePlace.Module = Instantiate(possibleModules[0].Prefab);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Block block = null;
                    if (hit.collider.TryGetComponent(out block))
                    {
                        Destroy(block.gameObject);
                    }

                    Module module = null;
                    if (hit.collider.TryGetComponent(out module))
                    {
                        Destroy(module.gameObject);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SaveStructurePattern();
            }

            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                LoadStructurePattern();
            }
        }


        private void SaveStructurePattern()
        {
            List<SerializableBlockBuildingData> blocksBuildingData = new List<SerializableBlockBuildingData>();
            List<Vector2Int> occupied = new List<Vector2Int>();

            for (int i = 0; i < cells.Length; i++)
            {
                if (cells[i].Block != null)
                {
                    Block block = cells[i].Block;
                    List<Module> modules = block.GetModules();
                    List<SerializableModuleBuildingData> modulesBuildingData = new List<SerializableModuleBuildingData>();

                    for (int p = 0; p < modules.Count; p++)
                    {
                        modulesBuildingData.Add(new SerializableModuleBuildingData(
                            PrefabsArchive.Instance.GetIndex(modules[p]),
                            block.GetModulePlaceID(modules[p])
                            ));
                    }

                    blocksBuildingData.Add(new SerializableBlockBuildingData(
                        PrefabsArchive.Instance.GetIndex(block),
                        cells[i].PositionID,
                        modulesBuildingData.ToArray()
                        ));
                }
            }

            SerializableStructureBuildingData structureBuildingData = new SerializableStructureBuildingData(
                    blocksBuildingData.ToArray(),
                    EPlace.Elevation,
                    "Debug Structure"
                    );

            string json = JsonUtility.ToJson(structureBuildingData);
            File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "structureBuildingData.txt"), json);
        }


        private void LoadStructurePattern()
        {
            string json = File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "structureBuildingData.txt"));
            SerializableStructureBuildingData structureBuildingData = JsonUtility.FromJson<SerializableStructureBuildingData>(json);

            Structure structure = structureBuildingData.CreateInstance();
            structure.transform.SetParent(transform);
            structure.Init(null, "Player");
        }
    }
}