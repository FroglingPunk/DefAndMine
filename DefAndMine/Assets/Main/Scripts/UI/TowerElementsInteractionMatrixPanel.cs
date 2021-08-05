using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerElementsInteractionMatrixPanel : MonoBehaviour
{
    [SerializeField] private Image[] towerElementIcons;
    [SerializeField] private GameObject panel;
    [SerializeField] private UIMatrixElement elementPrefab;
    [SerializeField] private GridLayoutGroup grid;

    private List<UIMatrixElement> uiMatrixElements = new List<UIMatrixElement>();


    void Update()
    {
        if (panel.activeSelf)
        {
            if (Input.GetMouseButtonDown(1))
            {
                Hide();
            }

            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            int layerMask = 1 << 12;

            if (Physics.Raycast(ray, out hit, 100f, layerMask))
            {
                if (hit.collider.TryGetComponent(out TowerShooter tower))
                {
                    Show(tower);
                }
            }
        }
    }


    public void Show(TowerShooter tower)
    {
        panel.SetActive(true);

        Sprite towerElementSprite = PrefabsArchive.Instance.GetElementIcon(tower.Element);
        for (int i = 0; i < towerElementIcons.Length; i++)
        {
            towerElementIcons[i].sprite = towerElementSprite;
        }

        uiMatrixElements.ForEach((uiMatrixElement) => Destroy(uiMatrixElement.gameObject));
        uiMatrixElements.Clear();

        ElementsInteractionMatrix matrix = tower.ElementsInteractionMatrix;
        for (EElement element = EElement.Fire; element < EElement.COUNT; element++)
        {
            UIMatrixElement uiMatrixElement = Instantiate(elementPrefab, grid.transform);
            uiMatrixElement.Init(element, matrix.IsAllowed(element),
                (changedElement, allowed) => matrix.Set(changedElement, allowed));
            uiMatrixElements.Add(uiMatrixElement);
        }
    }

    public void Hide()
    {
        panel.SetActive(false);
    }
}