using UnityEngine;

namespace Contstructor
{
    public abstract class Module : MonoBehaviour
    {
        [SerializeField] private int powerReceived;
        [SerializeField] private int powerConsumed;

        public int PowerReceived => powerReceived;
        public int PowerConsumed => powerConsumed;
        public Block Block { get; private set; }
        public virtual bool IsActive => powerConsumed == 0 || Block.IsActive;


        public virtual void Init(Block block)
        {
            Block = block;
        }
    }
}