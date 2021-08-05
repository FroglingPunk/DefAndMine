using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Elemental[] prefabs;
    [SerializeField] private Vector3[] wayPoints;


    void Update()
    {
        for (int i = 0; i < prefabs.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)(49 + i)))
            {
                Elemental elemental = Instantiate(prefabs[i], spawnPoint);
                elemental.transform.localPosition = Vector3.zero;
                elemental.Init(wayPoints);
            }
        }
    }
}