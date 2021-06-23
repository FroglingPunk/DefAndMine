using UnityEngine;

namespace Constructor.Structures
{
    public interface IStructureBuildingData
    {
        Vector2Int[] Occupied { get; }
        EPlace Place { get; }
        SerializableResourcesPackage Cost { get; }


        Structure CreateInstance();
    }
}