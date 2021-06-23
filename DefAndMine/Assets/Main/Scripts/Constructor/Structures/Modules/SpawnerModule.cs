using System.Collections.Generic;
using UnityEngine;

namespace Constructor.Structures
{
    public class SpawnerModule : Module
    {
        [SerializeField] private UnitSpawner unitSpawner;
        [SerializeField] private Unit unitPrefab;
        [SerializeField] private float delayBetweenSpawn = 5f;

        private float timeBetweenLastSpawn;


        void OnValidate()
        {
            if (unitSpawner == null)
            {
                unitSpawner = GetComponent<UnitSpawner>();

                if (unitSpawner == null)
                {
                    unitSpawner = GetComponentInChildren<UnitSpawner>();
                }
            }
        }

        void Update()
        {
            if (IsActive)
            {
                timeBetweenLastSpawn += Time.deltaTime;

                if (timeBetweenLastSpawn >= delayBetweenSpawn)
                {
                    timeBetweenLastSpawn = 0f;
                    unitSpawner.Spawn(unitPrefab);
                }
            }
        }


        public override void Init(Block block)
        {
            base.Init(block);
            unitSpawner = GetComponent<UnitSpawner>();

            unitSpawner.SetDestination(GameObject.Find("Debug Camp [" + (Block.Structure.Team == "Player" ? "Enemy]" : "Player]")).GetComponent<Structure>());

            Cell[] occupiedCells = block.Structure.Occupied;
            for (int i = 0; i < occupiedCells.Length; i++)
            {
                for (EDirection dir = EDirection.N; dir <= EDirection.NW; dir++)
                {
                    Cell neighbor = occupiedCells[i].GetNeighbor(dir);

                    if (neighbor != null && neighbor.Place == EPlace.Ground)
                    {
                        unitSpawner.SetSpawnPoint(occupiedCells[i].GetNeighbor(dir).transform);
                        return;
                    }
                }
            }
        }
    }

    public class PlayerUnitsStorage
    {
        public static List<Unit> Units { get; private set; }
    }
}