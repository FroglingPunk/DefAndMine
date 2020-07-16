using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StructureBuildingData", menuName = "Patterns/Structures/Structure Building Data", order = 51)]
public class StructureBuildingData : ScriptableObject
{
    [SerializeField]
    private Structure prefab;
    [SerializeField]
    private Vector2Int[] occupied;


    public Structure Prefab => prefab;
    public Vector2Int[] Occupied => occupied;
}