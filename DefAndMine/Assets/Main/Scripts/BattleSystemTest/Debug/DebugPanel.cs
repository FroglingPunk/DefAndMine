using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugPanel : MonoBehaviour
{
    [SerializeField] private Elemental elemental;

    [SerializeField] private List<DebugDamageButton> damageButtons;


    void Awake()
    {
        damageButtons.ForEach(
            (damageButton) => damageButton.button.onClick.AddListener(
                () =>
                elemental.GetDamage(damageButton.damageValue, damageButton.damageType, null)
                )
            );
    }


    [Serializable]
    public class DebugDamageButton
    {
        public Button button;
        public EDamageType damageType;
        public int damageValue;
    }
}