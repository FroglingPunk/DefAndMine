using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StructureBuildElementView : MonoBehaviour
{
    [field: SerializeField] public Button Button { get; private set; }
    [field: SerializeField] public TextMeshProUGUI TextName { get; private set; }
}