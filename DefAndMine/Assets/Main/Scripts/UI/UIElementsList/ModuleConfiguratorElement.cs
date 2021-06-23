using Constructor.Units;
using System;
using UIElementsList;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Constructor.Units
{
    // Data is ModuleBuildingData
    public class ModuleConfiguratorElement : UIListElement
    {
        [SerializeField] private Image imageIcon;


        public override void Init(object data, Action<object> onPointerClickCallback, Action<object> onPointerEnterCallback, Action<object> onPointerExitCallback)
        {
            base.Init(data, onPointerClickCallback, onPointerEnterCallback, onPointerExitCallback);

            imageIcon.sprite = (data as ModuleBuildingData).Icon;
        }
    }
}