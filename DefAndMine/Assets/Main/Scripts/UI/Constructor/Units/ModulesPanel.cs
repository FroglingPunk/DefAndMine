using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Constructor.Units;
using System;

namespace UI.Constructor.Units
{
    public class ModulesPanel : MonoBehaviour
    {
        [SerializeField] private ModuleElement[] elements;

        public List<ModuleBuildingData> Modules
        {
            get
            {
                List<ModuleBuildingData> modules = new List<ModuleBuildingData>();

                for (int i = 0; i < elements.Length; i++)
                {
                    if (elements[i].Module != null)
                    {
                        modules.Add(elements[i].Module);
                    }
                }

                return modules;
            }
        }

        public event Action<ModuleBuildingData> OnModuleChanged;
        public event Action<ModuleElement> OnModuleElementButtonClick;

        private Dictionary<EModuleType, ModuleElement> elementsDictionary;


        void Awake()
        {
            elementsDictionary = new Dictionary<EModuleType, ModuleElement>();

            for (int i = 0; i < elements.Length; i++)
            {
                ModuleElement element = elements[i];

                elementsDictionary.Add(element.ModuleType, element);

                element.OnModuleChanged += (module) =>
                {
                    // добавил проверку на наличие элемента с UnitBody,
                    // так как при OnEnable все элементы обновляют свои значения Module и вызывают event,
                    // при этом UnitBody элемент к этому моменту может ещё не иметь значения
                    if (elementsDictionary[EModuleType.Body].Module != null)
                    {
                        OnModuleChanged?.Invoke(module);
                    }
                };

                element.OnButtonClick += OnModuleElementButtonClick;
            }
        }


        public void SetModule(ModuleBuildingData module)
        {
            elementsDictionary[module.Prefab.Type].SetModule(module);
        }
    }
}