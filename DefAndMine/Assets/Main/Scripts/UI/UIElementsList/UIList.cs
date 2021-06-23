using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UIElementsList
{
    public class UIList : MonoBehaviour
    {
        [SerializeField] private UIListElement elementPrefab;
        [SerializeField] private ScrollRect scrollRect;

        private List<UIListElement> elements = new List<UIListElement>();

        public event Action<object> OnElementPointerClick;
        public event Action<object> OnElementPointerEnter;
        public event Action<object> OnElementPointerExit;


        public void Init(object[] elementsData, Action<object> onPointerClick, Action<object> onPointerEnter, Action<object> onPointerExit)
        {
            Clear();

            OnElementPointerClick = onPointerClick;
            OnElementPointerEnter = onPointerEnter;
            OnElementPointerExit = onPointerExit;

            for (int i = 0; i < elementsData.Length; i++)
            {
                Add(elementsData[i]);
            }
        }

        public void Add(object elementData)
        {
            UIListElement newElement = Instantiate(elementPrefab, scrollRect.content);
            newElement.Init(elementData, OnElementPointerClick, OnElementPointerEnter, OnElementPointerExit);
            elements.Add(newElement);
        }

        public void Clear()
        {
            for (int i = 0; i < elements.Count; i++)
            {
                Destroy(elements[i].gameObject);
            }

            elements.Clear();
        }
    }
}