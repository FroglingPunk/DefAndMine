using UnityEngine;

namespace Contstructor
{
    [CreateAssetMenu(fileName = "BlockBuildingData", menuName = "Patterns/Constructor/Block Building Data", order = 51)]
    public class BlockBuildingData : ScriptableObject, IBlockBuildingData
    {
        [SerializeField] private Block prefab;


        public Block Prefab => prefab;
    }
}