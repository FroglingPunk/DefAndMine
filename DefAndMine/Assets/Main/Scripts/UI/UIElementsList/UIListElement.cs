using UnityEngine.EventSystems;
using UnityEngine;
using System;

namespace UIElementsList
{
    public abstract class UIListElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public object Data { get; private set; }

        public event Action<object> OnPointerClickCallback;
        public event Action<object> OnPointerEnterCallback;
        public event Action<object> OnPointerExitCallback;


        public virtual void Init(object data, Action<object> onPointerClickCallback, Action<object> onPointerEnterCallback, Action<object> onPointerExitCallback)
        {
            Data = data;

            OnPointerClickCallback = onPointerClickCallback;
            OnPointerEnterCallback = onPointerEnterCallback;
            OnPointerExitCallback = onPointerExitCallback;
        }


        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEnterCallback?.Invoke(Data);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExitCallback?.Invoke(Data);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClickCallback?.Invoke(Data);
        }
    }
}