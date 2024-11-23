using System;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnerManager : MonoBehaviour
{
    [Serializable]
    private class NumeratedUnitSpawner
    {
        public UnitSpawner spawner;
        public int id;
    }


    [SerializeField] private NumeratedUnitSpawner[] numeratedUnitSpawners;

    private Dictionary<int, UnitSpawner> spawners = new Dictionary<int, UnitSpawner>();


    void Awake()
    {
        for (int i = 0; i < numeratedUnitSpawners.Length; i++)
        {
            spawners.Add(numeratedUnitSpawners[i].id, numeratedUnitSpawners[i].spawner);
        }
    }


    public UnitSpawner GetSpawner(int id)
    {
        return spawners.ContainsKey(id) ? spawners[id] : numeratedUnitSpawners[0].spawner;
    }
}