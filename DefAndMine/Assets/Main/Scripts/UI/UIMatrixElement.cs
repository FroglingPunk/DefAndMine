using System;
using UnityEngine;
using UnityEngine.UI;

public class UIMatrixElement : MonoBehaviour
{
    [SerializeField] private Toggle toggle;
    [SerializeField] private Image imageIcon;


    public void Init(EElement element, bool allowed, Action<EElement, bool> onToggleChanged)
    {
        imageIcon.sprite = PrefabsArchive.Instance.GetElementIcon(element);

        toggle.isOn = allowed;
        toggle.onValueChanged.AddListener((value) => onToggleChanged(element, value));
    }
}