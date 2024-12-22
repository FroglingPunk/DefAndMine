using System.Collections.Generic;
using UnityEngine;

public class StructureInstaller : InstallerBase
{
    [SerializeField] private LineRenderer _powerLineTemplate;
    
    public override List<IController> RegisterControllers()
    {
        var container = LocalSceneContainer.Instance.ControllersContainer;

        return new List<IController>()
        {
            container.AddController(new StructuresController()),
            container.AddController(new PowerTransitModulesHandler(_powerLineTemplate))
        };
    }
}