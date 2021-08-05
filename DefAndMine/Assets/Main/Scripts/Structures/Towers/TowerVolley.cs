using UnityEngine;

public class TowerVolley : Structure
{
    [SerializeField] private EPlace place = EPlace.Ground;
    [SerializeField] private ElementsInteractionMatrix elementsInteractionMatrix;
    [SerializeField] private EElement element;

    public override EPlace Place => place;
    public ElementsInteractionMatrix ElementsInteractionMatrix => elementsInteractionMatrix;
    public override EElement Element => element;

    private InfluenceArea influenceArea;
    private float timeFromLastAttack = 0f;
    private float delayBetweenAttacks = 10f;
    private int damage = 10;


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
    }

    void Update()
    {
        timeFromLastAttack += Time.deltaTime;

        if (timeFromLastAttack >= delayBetweenAttacks)
        {
            if (TryAttack())
            {
                timeFromLastAttack = 0f;
            }
        }
    }


    private bool TryAttack()
    {
        bool containAllowedElemental = false;
        for (int i = 0; i < influenceArea.Inside.Count; i++)
        {
            Elemental elementalInside = influenceArea.Inside[i];

            if (ElementsInteractionMatrix.IsAllowed(elementalInside.Element))
            {
                containAllowedElemental = true;
                break;
            }
        }

        if (containAllowedElemental)
        {
            for (int i = influenceArea.Inside.Count - 1; i >= 0; i--)
            {
                influenceArea.Inside[i].GetImpact(element, damage);
            }

            return true;
        }
        else
        {
            return false;
        }
    }
}