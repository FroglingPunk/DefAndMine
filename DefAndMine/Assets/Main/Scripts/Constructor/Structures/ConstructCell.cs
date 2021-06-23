using UnityEngine;

namespace Constructor.Structures
{
    public class ConstructCell : MonoBehaviour
    {
        [SerializeField] private Vector2Int positionID;
        public Vector2Int PositionID => positionID;

        private Block block;
        public Block Block
        {
            get
            {
                return block;
            }
            set
            {
                if (value == null)
                {
                    if (block != null)
                    {
                        Destroy(block.gameObject);
                    }
                }
                else
                {
                    value.transform.SetParent(transform);
                    value.transform.localPosition = Vector3.zero;
                    value.transform.localRotation = Quaternion.identity;
                }

                block = value;
            }
        }
    }
}