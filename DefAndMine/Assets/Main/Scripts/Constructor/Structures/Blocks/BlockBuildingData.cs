using UnityEngine;

namespace Constructor.Structures
{
    [CreateAssetMenu(fileName = "BlockBuildingData", menuName = "Patterns/Constructor/Structures/Block Building Data", order = 51)]
    public class BlockBuildingData : ScriptableObject, IBlockBuildingData
    {
        [SerializeField] private Block prefab;


        public Block Prefab => prefab;
    }
}