using System;
using UnityEngine;
using Contstructor.Utils;

namespace Contstructor
{
    [RequireComponent(typeof(InfluenceArea))]
    public class ShooterModule : Module
    {
        [Serializable]
        public class ShooterDot
        {
            [SerializeField] private float reloadTime = 3f;
            [SerializeField] private int damage = 12;

            private float breakBetweenShots;


            public void Update(InfluenceArea influenceArea)
            {
                breakBetweenShots += Time.deltaTime;
                if (breakBetweenShots > reloadTime)
                {
                    Unit unit = influenceArea.GetRandomUnit;

                    if (unit != null)
                    {
                        unit.GetDamage(damage);
                        breakBetweenShots = 0f;
                    }
                }
            }
        }

        [SerializeField] private ShooterDot[] shootersDots;
        [SerializeField] private InfluenceArea influenceArea;


        void OnValidate()
        {
            if(influenceArea == null)
            {
                influenceArea = GetComponent<InfluenceArea>();

                if(influenceArea == null)
                {
                    influenceArea = GetComponentInChildren<InfluenceArea>();
                }
            }
        }

        void Update()
        {
            if (IsActive)
            {
                for(int i = 0; i < shootersDots.Length; i++)
                {
                    shootersDots[i].Update(influenceArea);
                }
            }
        }


        public override void Init(Block block)
        {
            base.Init(block);
            influenceArea = GetComponent<InfluenceArea>();
        }
    }
}