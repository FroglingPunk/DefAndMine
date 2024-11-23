using Constructor.Structures;
using UnityEngine;
using UIElementsList;

public class StructuresPanel : MonoBehaviour
{
    // debug only
    [SerializeField] private StructureBuildingData[] buildingsData;
    //

    [SerializeField] UIList structuresList;

    public StructureBuildingData PickedStructureBuildingData { get; private set; }


    void Start()
    {
        structuresList.Init(buildingsData, (obj) => OnStructurePicked((StructureBuildingData)obj), null, null);
    }


    private void OnStructurePicked(StructureBuildingData buildingData)
    {
        PickedStructureBuildingData = buildingData;
    }
}