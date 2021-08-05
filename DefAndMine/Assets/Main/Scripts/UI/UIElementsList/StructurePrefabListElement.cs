using System;
using UnityEngine.UI;

namespace UIElementsList
{
    public class StructurePrefabListElement : UIListElement
    {
        public override void Init(object data, Action<object> onPointerClickCallback, Action<object> onPointerEnterCallback, Action<object> onPointerExitCallback)
        {
            base.Init(data, onPointerClickCallback, onPointerEnterCallback, onPointerExitCallback);

            GetComponent<Image>().sprite = PrefabsArchive.Instance.GetElementIcon((data as Structure).Element);
        }
    }
}