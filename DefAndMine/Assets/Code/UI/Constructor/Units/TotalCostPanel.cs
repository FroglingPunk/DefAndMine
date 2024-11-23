using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Constructor.Units;

namespace UI.Constructor.Units
{
    public class TotalCostPanel : MonoBehaviour
    {
        [Serializable]
        private class ResourceElement
        {
            public Text uiText;
            public EResource resource;
        }

        [SerializeField] private ModulesPanel modulesPanel;
        [SerializeField] private ResourceElement[] resourcesElements;


        void Awake()
        {
            modulesPanel.OnModuleChanged += (module) => UpdateCost();
            UpdateCost();
        }


        private void UpdateCost()
        {
            List<ModuleBuildingData> modules = modulesPanel.Modules;

            ResourcesStorage totalCost = new ResourcesStorage();
            for (int i = 0; i < modules.Count; i++)
            {
                totalCost.Increase(modules[i].Cost.All);
            }

            for (int i = 0; i < resourcesElements.Length; i++)
            {
                resourcesElements[i].uiText.text = totalCost[resourcesElements[i].resource].ToString();
            }
        }
    }
}