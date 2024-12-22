using System.Collections.Generic;
using UnityEngine;

public class UIInstaller : InstallerBase
{
    [SerializeField] private Transform _uiControllerViewsParent;

    private void Reset()
    {
        _uiControllerViewsParent = FindObjectOfType<Canvas>()?.transform.parent;
    }


    public override List<IController> RegisterControllers()
    {
        var uiControllerViews = _uiControllerViewsParent.GetComponentsInChildren<UIControllerView>(true);
        var controllers = new List<IController>(uiControllerViews.Length * 2);

        var container = LocalSceneContainer.Instance.ControllersContainer;

        for (var i = 0; i < uiControllerViews.Length; i++)
        {
            var view = uiControllerViews[i];
            var model = view.CreateModel();

            controllers.Add(container.AddControllerAs(view, view.GetType()));
            controllers.Add(container.AddControllerAs(model, model.GetType()));
        }

        return controllers;
    }
}