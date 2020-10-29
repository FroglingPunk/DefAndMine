using System.Collections.Generic;
using UnityEngine;

namespace Contstructor.Utils
{
    [RequireComponent(typeof(SphereCollider))]
    public class InfluenceArea : MonoBehaviour
    {
        public List<Unit> UnitsInside { get; private set; }
        public Unit GetRandomUnit
        {
            get
            {
                if (UnitsInside.Count > 0)
                {
                    return UnitsInside[Random.Range(0, UnitsInside.Count)];
                }

                return null;
            }
        }


        void Awake()
        {
            UnitsInside = new List<Unit>();
        }

        void OnTriggerEnter(Collider other)
        {
            Unit unit = null;
            if (other.TryGetComponent(out unit))
            {
                UnitsInside.Add(unit);
                unit.OnDie += OnUnitDie;
            }
        }

        void OnTriggerExit(Collider other)
        {
            Unit unit = null;
            if (other.TryGetComponent(out unit))
            {
                UnitsInside.Remove(unit);
                unit.OnDie += OnUnitDie;
            }
        }


        private void OnUnitDie(Unit unit)
        {
            unit.OnDie -= OnUnitDie;
            UnitsInside.Remove(unit);
        }
    }
}