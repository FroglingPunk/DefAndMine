using System;
using UnityEngine;
using Constructor.Utils;

namespace Constructor.Structures
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
            private Unit target;

            public void Update(InfluenceArea influenceArea, Structure structure, LineRenderer lineRenderer)
            {
                breakBetweenShots += Time.deltaTime;
                if (breakBetweenShots > reloadTime)
                {
                    if (target == null || !influenceArea.UnitsInside.Contains(target))
                    {
                        target = influenceArea.GetRandomUnit(structure.Team == "Enemy" ? "Player" : "Enemy");
                    }

                    if (target != null)
                    {
                        target.GetDamage(damage);
                        breakBetweenShots = 0f;
                    }
                }

                if (target != null)
                {
                    lineRenderer.SetPositions(new Vector3[] { structure.transform.position, target.transform.position });
                }
                else
                {
                    lineRenderer.SetPositions(new Vector3[] { structure.transform.position, structure.transform.position });
                }
            }
        }

        [SerializeField] private ShooterDot[] shootersDots;
        [SerializeField] private InfluenceArea influenceArea;

        //debug visual
        [SerializeField] private LineRenderer lineRenderer;
        //

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
                    shootersDots[i].Update(influenceArea, Block.Structure, lineRenderer);
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