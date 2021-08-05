using UnityEngine;

public class ElementalUnificationPlace : MonoBehaviour
{
    private InfluenceArea influenceArea;


    void OnValidate()
    {
        if (GetComponentInChildren<InfluenceArea>() == null)
        {
            Transform influenceAreaTransform = new GameObject("InfluenceArea", typeof(InfluenceArea)).transform;

            influenceAreaTransform.parent = transform;
            influenceAreaTransform.localPosition = Vector3.zero;

            influenceAreaTransform.GetComponent<Collider>().isTrigger = true;
        }
    }

    void Awake()
    {
        influenceArea = GetComponentInChildren<InfluenceArea>();
        influenceArea.OnEnter += OnEnter;
    }

    void OnDestroy()
    {
        influenceArea.OnEnter -= OnEnter;
    }


    private void OnEnter(Elemental entered)
    {
        TryUnite();
    }

    private void TryUnite()
    {
        for (int i = 0; i < influenceArea.Inside.Count; i++)
        {
            for (int p = i + 1; p < influenceArea.Inside.Count; p++)
            {
                Elemental elementalA = influenceArea.Inside[i];
                Elemental elementalB = influenceArea.Inside[p];

                if (Elemental.Unite(elementalA, elementalB))
                {
                    return;
                }
            }
        }
    }
}