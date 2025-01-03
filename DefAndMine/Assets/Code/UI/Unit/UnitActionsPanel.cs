using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitActionsPanel : MonoBehaviour
{
    [SerializeField] private Button _buttonCancelAction;
    [SerializeField] private UnitActionElement _elementPrefab;
    [SerializeField] private RectTransform _elementsParent;

    private readonly Queue<UnitActionElement> _poolElements = new();
    private readonly List<UnitActionElement> _activeElements = new();

    public event Action<UnitAction> OnActionSelect;


    private void Start()
    {
        _buttonCancelAction.onClick.AddListener(() => OnElementClick(null));
    }


    public void Show(Unit unit)
    {
        gameObject.SetActive(true);
        Clear();

        for (var i = 0; i < unit.Context.Actions.Count; i++)
        {
            var element = GetElement();
            element.Show(unit.Context.Actions[i]);
        }
    }

    public void Hide()
    {
        gameObject.SetActive(false);
        _buttonCancelAction.gameObject.SetActive(false);
    }

    public void Clear()
    {
        for (var i = _activeElements.Count - 1; i >= 0; i--)
        {
            ReleaseElement(_activeElements[i]);
        }
    }


    private UnitActionElement GetElement()
    {
        if (_poolElements.Count > 0)
        {
            var element = _poolElements.Dequeue();
            element.gameObject.SetActive(true);
            _activeElements.Add(element);
            return element;
        }
        else
        {
            var element = Instantiate(_elementPrefab, _elementsParent);
            element.OnClick += OnElementClick;
            _activeElements.Add(element);
            return element;
        }
    }

    private void ReleaseElement(UnitActionElement element)
    {
        element.gameObject.SetActive(false);
        _poolElements.Enqueue(element);
        _activeElements.Remove(element);
    }

    private void OnElementClick(UnitActionElement element)
    {
        _buttonCancelAction.gameObject.SetActive(element != null);
        OnActionSelect?.Invoke(element == null ? null : element.Action);
    }
}