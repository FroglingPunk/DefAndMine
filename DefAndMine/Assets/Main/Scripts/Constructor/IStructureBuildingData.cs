using UnityEngine;

namespace Contstructor
{
    public interface IStructureBuildingData
    {
        Vector2Int[] Occupied { get; }
        EPlace Place { get; }


        Structure CreateInstance();
    }
}