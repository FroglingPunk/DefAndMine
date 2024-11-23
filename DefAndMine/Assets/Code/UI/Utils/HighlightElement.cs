using UnityEngine;
using UnityEngine.EventSystems;

public class HighlightElement : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private GameObject textHighlight;


    void Start()
    {
        textHighlight.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        textHighlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        textHighlight.SetActive(false);
    }

    private void OnDisable()
    {
        textHighlight.SetActive(false);
    }
}