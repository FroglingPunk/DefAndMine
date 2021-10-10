using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementInteractionMatrix : MonoBehaviour
{
    #region Debug Utilities
    [SerializeField] private bool CREATE_RESULTS = false;
    private void OnValidate()
    {
        if (CREATE_RESULTS)
        {
            CREATE_RESULTS = false;

            for (EElement elemental = 0; elemental < EElement.COUNT; elemental++)
            {
                Transform elementalTransform = new GameObject("ELEMENTAL [" + elemental + "]").transform;
                elementalTransform.SetParent(transform);

                for (EElement impact = 0; impact < EElement.COUNT; impact++)
                {
                    Transform impactTransform = new GameObject("IMPACT [" + impact + "]").transform;
                    impactTransform.SetParent(elementalTransform);

                    ElementsInteraction interaction = impactTransform.gameObject.AddComponent<ElementsInteraction>();
                    interaction.elemental = elemental;
                    interaction.impact = impact;
                }
            }
        }
    }
    #endregion


    private static ElementInteractionMatrix instance;
    public static ElementInteractionMatrix Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<ElementInteractionMatrix>();

                if (instance == null)
                {
                    instance = Instantiate(Resources.Load<ElementInteractionMatrix>("ElementInteractionMatrix"));
                }

                DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }


    private ElementsInteraction[] interactions;


    void Awake()
    {
        interactions = GetComponentsInChildren<ElementsInteraction>();
    }


    public ElementsInteraction GetElementsInteraction(EElement elemental, EElement impact)
    {
        ElementsInteraction interaction = null;

        for (int i = 0; i < interactions.Length; i++)
        {
            interaction = interactions[i];

            if (interaction.elemental == elemental && interaction.impact == impact)
            {
                return interaction;
            }
        }

        return null;
    }
}