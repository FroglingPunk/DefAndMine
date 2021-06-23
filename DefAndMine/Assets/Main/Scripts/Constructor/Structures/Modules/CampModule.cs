using UnityEngine;
using Constructor.Utils;

namespace Constructor.Structures
{
    [RequireComponent(typeof(InfluenceArea))]
    public class CampModule : Module
    {
        [SerializeField] private InfluenceArea influenceArea;


        void OnValidate()
        {
            if (influenceArea == null)
            {
                influenceArea = GetComponent<InfluenceArea>();

                if (influenceArea == null)
                {
                    influenceArea = GetComponentInChildren<InfluenceArea>();
                }
            }
        }

        //void Start()
        //{
        //    //influenceArea.OnUnitEnter +=
        //}

        public override void Init(Block block)
        {
            base.Init(block);

            if (influenceArea == null)
            {
                influenceArea = GetComponent<InfluenceArea>();
            }
        }


        private void OnUnitEnter(Unit unit)
        {
            if (unit.Team != Block.Structure.Team)
            {
                
            }
        }
    }
}