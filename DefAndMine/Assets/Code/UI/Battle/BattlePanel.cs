using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class BattlePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textRoundNumber;
    [SerializeField] private ToggleGroup _toggleGroupStage;

    private CompositeDisposable _disposables = new();


    private void Start()
    {
        _disposables.Add(BattleTurnSystem.Instance.RoundNumber.Subscribe(rn => _textRoundNumber.text = $"Раунд #{rn}"));
        _disposables.Add(BattleTurnSystem.Instance.Stage.Subscribe(stage => _toggleGroupStage.transform.GetChild((int)stage).GetComponent<Toggle>().isOn = true));
    }

    private void OnDestroy()
    {
        _disposables?.Dispose();
    }
}