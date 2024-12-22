using System.Collections.Generic;
using UnityEngine;

public class FieldInstaller : InstallerBase
{
    [SerializeField] private Transform _fieldTransform;
    
    public override List<IController> RegisterControllers()
    {
        var container = LocalSceneContainer.Instance.ControllersContainer;

        return new List<IController>()
        {
            container.AddController(new FieldController(_fieldTransform)),
        };
    }
}