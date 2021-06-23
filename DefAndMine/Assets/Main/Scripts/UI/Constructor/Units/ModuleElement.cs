using Constructor.Units;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI.Constructor.Units
{
    public class ModuleElement : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Image imageIcon;
        [SerializeField] private EModuleType moduleType;
        [SerializeField] private ModuleBuildingData defaultModule;

        private ModuleBuildingData module;
        public ModuleBuildingData Module
        {
            get => module;
            private set
            {
                module = value;
                OnModuleChanged?.Invoke(value);
            }
        }

        public EModuleType ModuleType => moduleType;

        public Action<ModuleBuildingData> OnModuleChanged;
        public Action<ModuleElement> OnButtonClick;


        void Awake()
        {
            button.onClick.AddListener(() => OnButtonClick(this));
        }
        
        void OnEnable()
        {
            SetModule(defaultModule);
        }


        public void SetModule(ModuleBuildingData module)
        {
            Module = module;

            if (module != null)
            {
                imageIcon.enabled = true;
                imageIcon.sprite = module.Icon;
            }
            else
            {
                imageIcon.enabled = false;
            }
        }
    }
}