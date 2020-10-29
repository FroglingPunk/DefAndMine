using UnityEngine;
using UnityEngine.Assertions;

namespace Contstructor
{
    public class Structure : MonoBehaviour
    {
        private Block[] blocks;

        private Cell[] occupied;
        public Cell[] Occupied
        {
            get
            {
                return occupied;
            }
            set
            {
                if (occupied != null)
                {
                    for (int i = 0; i < occupied.Length; i++)
                    {
                        occupied[i].Structure = null;
                    }
                }

                occupied = value;

                if (occupied != null)
                {
                    for (int i = 0; i < occupied.Length; i++)
                    {
                        Assert.IsNull(occupied[i].Structure.Value);

                        occupied[i].Structure.Value = this;
                    }
                }
            }
        }

        public bool IsActive => true;
        public int PowerConsumed
        {
            get
            {
                int powerConsumed = 0;

                for (int i = 0; i < blocks.Length; i++)
                {
                    powerConsumed += blocks[i].PowerConsumed;
                }

                return powerConsumed;
            }
        }
        public int PowerReceived
        {
            get
            {
                int powerReceived = 0;

                for (int i = 0; i < blocks.Length; i++)
                {
                    powerReceived += blocks[i].PowerReceived;
                }

                return powerReceived;
            }
        }


        public void Init(Cell[] occupiedCells)
        {
            blocks = GetComponentsInChildren<Block>();

            Occupied = occupiedCells;

            for (int i = 0; i < blocks.Length; i++)
            {
                blocks[i].Init(this);
            }
        }
    }
}