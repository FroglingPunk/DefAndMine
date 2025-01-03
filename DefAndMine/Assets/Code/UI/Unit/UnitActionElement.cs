using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionElement : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _imageIcon;
    [SerializeField] private TextMeshProUGUI _textName;

    public UnitAction Action { get; private set; }
    
    public event Action<UnitActionElement> OnClick;

    private void Start()
    {
        _button.onClick.AddListener(() => OnClick?.Invoke(this));
    }


    public void Show(UnitAction action)
    {
        Action = action;
        
        _imageIcon.sprite = action.Icon;
        _textName.text = action.Name;
    }
}