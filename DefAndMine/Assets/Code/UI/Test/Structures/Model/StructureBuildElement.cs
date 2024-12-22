using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class StructureBuildElement
{
    private readonly StructureBuildElementView _view;


    public StructureBuildElement(StructureBuildElementView template, RectTransform parent, StructureData data,
        Action<StructureData> onClickCallback)
    {
        _view = Object.Instantiate(template, parent);
        _view.TextName.text = data.Name;
        _view.Button.onClick.AddListener(() => onClickCallback?.Invoke(data));
    }
}