using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Transform destination;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Unit unitPrefab;
    

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Unit unit = Instantiate(unitPrefab, spawnPoint.position, Quaternion.identity);
            unit.Init(destination);
            unit.OnHealthChanged += OnUnitHealthChanged;
            unit.OnDie += OnUnitDie;
        }
    }

    void OnUnitHealthChanged(Unit unit)
    {
        Debug.Log("HEALTH : " + unit.Health);
    }

    void OnUnitDie(Unit unit)
    {
        Debug.Log(unit.gameObject.name + " IS DEAD");
    }
}