using UnityEngine;
using Constructor.Structures;
using UnityEngine.UI;

public class ChosenStructureUI : MonoBehaviour
{
    [SerializeField] private Transform uiTransform;

    [SerializeField] private Button buttonDemolish;
    [SerializeField] private Button buttonUpgrade;
    [SerializeField] private Button buttonCreateUnits;

    [SerializeField] private UnitCreatingPanel unitCreatingPanel;


    void Awake()
    {
        buttonDemolish.onClick.AddListener(OnButtonDemolishPressed);
        buttonUpgrade.onClick.AddListener(OnButtonUpgradePressed);
        buttonCreateUnits.onClick.AddListener(OnButtonCreateUnitsPressed);

        Control.Instance.OnStructureChosen += OnStructureChosen;
        Control.Instance.OnCancelButtonClick += OnCancelButtonClick;
    }

    void OnDestroy()
    {
        Control.Instance.OnStructureChosen -= OnStructureChosen;
        Control.Instance.OnCancelButtonClick -= OnCancelButtonClick;
    }


    private void OnCancelButtonClick()
    {
        uiTransform.gameObject.SetActive(false);
    }

    private void OnStructureChosen(Structure structure)
    {
        uiTransform.position = structure.transform.position;
        uiTransform.gameObject.SetActive(true);
    }


    private void OnButtonDemolishPressed()
    {
        if (Control.Instance.ChosenStructure != null)
        {
            Control.Instance.ChosenStructure.Demolish();
        }
    }

    private void OnButtonUpgradePressed()
    {
        if (Control.Instance.ChosenStructure != null)
        {
            //Control.Instance.ChosenStructure.Demolish();
        }
    }

    private void OnButtonCreateUnitsPressed()
    {
        if (Control.Instance.ChosenStructure != null)
        {
            unitCreatingPanel.Show();
        }
    }
}