using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Constructor.Units;
using UIElementsList;
using Constructor;

namespace UI.Constructor.Units
{
    public class ModuleConfiguratorPanel : MonoBehaviour
    {
        [SerializeField] private GameObject defaultView;
        [SerializeField] private GameObject modulesView;
        [SerializeField] private ModulesPanel modulesPanel;
        [SerializeField] private Text uiTextDescription;
        [SerializeField] private UIList uiList;


        void Awake()
        {
            modulesPanel.OnModuleElementButtonClick += OnModuleElementButtonClick;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                ShowDefaultView();
            }
        }


        private void ShowDefaultView()
        {
            modulesView.SetActive(false);
            defaultView.SetActive(true);
        }

        private void OnModuleElementButtonClick(ModuleElement moduleElement)
        {
            modulesView.SetActive(true);
            defaultView.SetActive(false);


            ModuleBuildingData[] allModules = PrefabsArchive.Instance.UnitsModulesData;
            List<ModuleBuildingData> needTypeModules = new List<ModuleBuildingData>();

            for (int i = 0; i < allModules.Length; i++)
            {
                if (allModules[i].Prefab.Type == moduleElement.ModuleType)
                {
                    needTypeModules.Add(allModules[i]);
                }
            }

            uiList.Init(needTypeModules.ToArray(),
                (obj) => OnModuleConfigClick(obj as ModuleBuildingData),
                (obj) => OnModuleConfigPointerEnter(obj as ModuleBuildingData),
                (obj) => OnModuleConfigPointerExit(obj as ModuleBuildingData));
        }


        private void OnModuleConfigClick(ModuleBuildingData module)
        {
            modulesPanel.SetModule(module);
        }

        private void OnModuleConfigPointerEnter(ModuleBuildingData module)
        {
            uiTextDescription.text = (module.Name + "\n\n" + module.Decsription);
        }

        private void OnModuleConfigPointerExit(ModuleBuildingData module)
        {
            uiTextDescription.text = string.Empty;
        }
    }
}