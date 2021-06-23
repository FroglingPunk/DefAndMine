using UnityEngine;

namespace Constructor.Structures
{
    [CreateAssetMenu(fileName = "StructureBuildingData", menuName = "Patterns/Constructor/Structures/Structure Building Data", order = 51)]
    [System.Serializable]
    public class StructureBuildingData : ScriptableObject, IStructureBuildingData
    {
        [SerializeField] private Structure prefab;
        [SerializeField] private Vector2Int[] occupied;
        [SerializeField] private EPlace place;
        [SerializeField] private SerializableResourcesPackage cost;


        public Vector2Int[] Occupied => occupied;
        public EPlace Place => place;
        public SerializableResourcesPackage Cost => cost;


        public Structure CreateInstance()
        {
            return Instantiate(prefab);
        }
    }
}