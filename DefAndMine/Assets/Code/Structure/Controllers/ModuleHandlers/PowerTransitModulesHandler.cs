using UnityEngine;

public class PowerTransitModulesHandler : ModulesHandlerBase
{
    private LineRenderer _lineTemplate;

    public PowerTransitModulesHandler(LineRenderer lineTemplate)
    {
        _lineTemplate = lineTemplate;
    }

    protected override void OnStructureAdded(StructureBase structure)
    {
        UpdateView();
    }

    protected override void OnStructureRemoved(StructureBase structure)
    {
        UpdateView();
    }


    private void UpdateView()
    {
        var generators = Object.FindObjectsOfType<PowerGeneratorStructure>();
        var field = LocalSceneContainer.GetController<FieldController>();

        for (var i = 0; i < generators.Length; i++)
        {
            var powerGen = generators[i];

            var transitModules = powerGen.GetCertainModules<PowerTransitModule>();

            for (var p = 0; p < transitModules.Count; p++)
            {
                var transit = transitModules[p];

                if (transit.TransitType == EPowerTransitType.Input)
                {
                    continue;
                }

                if (!field.TryGetFirstStructureByDirection(powerGen.Cell, transit.Direction,
                        out var structureByPowerRay))
                {
                    continue;
                }

                var receiver = structureByPowerRay.GetCertainModules<PowerTransitModule>().Find(m =>
                    m.TransitType == EPowerTransitType.Input & m.Direction.Opposite() == transit.Direction);

                if (receiver == null)
                {
                    continue;
                }

                var line = Object.Instantiate(_lineTemplate);
                line.gameObject.SetActive(true);
                var points = new Vector3[] { transit.transform.position, receiver.transform.position };
                line.positionCount = 2;
                line.SetPositions(points);
            }
        }
    }
}