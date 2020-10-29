using UnityEngine;

namespace Contstructor
{
    [CreateAssetMenu(fileName = "StructureBuildingData", menuName = "Patterns/Constructor/Structure Building Data", order = 51)]
    [System.Serializable]
    public class StructureBuildingData : ScriptableObject, IStructureBuildingData
    {
        [SerializeField] private Structure prefab;
        [SerializeField] private Vector2Int[] occupied;
        [SerializeField] private EPlace place;


        public Vector2Int[] Occupied => occupied;
        public EPlace Place => place;


        public Structure CreateInstance()
        {
            return Instantiate(prefab);
        }
    }
}