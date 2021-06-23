using UnityEngine;

namespace Constructor.Units
{
    public class UnitsBuilder : MonoBehaviour
    {
        private static Transform unitsTransformParent;
        private static Transform UnitsTransformParent
        {
            get
            {
                if (unitsTransformParent == null)
                {
                    unitsTransformParent = new GameObject("[UNITS]").transform;
                }

                return unitsTransformParent;
            }
        }

        [SerializeField] private Object unitBuildingData;


        void OnValidate()
        {
            if(unitBuildingData is IUnitBuildingData == false)
            {
                unitBuildingData = null;
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Build(unitBuildingData as IUnitBuildingData, "Player");
            }
        }


        public static bool Build(IUnitBuildingData buildingData, string team)
        {
            Unit unit = buildingData.CreateInstance();
            unit.transform.SetParent(UnitsTransformParent);
            //unit.Init();

            //structure.transform.position = fulcrum.transform.position;
            //unit.Init(occupiedCells, team);

            return true;
        }
    }
}