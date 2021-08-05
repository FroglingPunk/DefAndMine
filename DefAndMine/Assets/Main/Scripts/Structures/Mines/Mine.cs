using UnityEngine;

public class Mine : Structure
{
    [SerializeField] private EPlace place = EPlace.Ground;
    [SerializeField] private ElementsInteractionMatrix elementsInteractionMatrix;
    [SerializeField] private EElement element;
    [SerializeField] private float maxLifeTime = 20f;
    [SerializeField] private int damage = 10;
    [SerializeField] private float delay = 1f;

    public override EPlace Place => place;
    public ElementsInteractionMatrix ElementsInteractionMatrix => elementsInteractionMatrix;
    public override EElement Element => element;

    private InfluenceArea influenceArea;
    private float lifeTime = 0f;


    void OnValidate()
    {
        if (GetComponentInChildren<InfluenceArea>() == null)
        {
            Transform influenceAreaTransform = new GameObject("InfluenceArea", typeof(InfluenceArea)).transform;

            influenceAreaTransform.parent = transform;
            influenceAreaTransform.localPosition = Vector3.zero;

            // layer 2 = IgnoreRaycast
            influenceAreaTransform.gameObject.layer = 2;

            influenceAreaTransform.GetComponent<Collider>().isTrigger = true;
        }
    }

    void Awake()
    {
        influenceArea = GetComponentInChildren<InfluenceArea>();
    }

    void Update()
    {
        lifeTime += Time.deltaTime;

        if (lifeTime >= maxLifeTime)
        {
            Explode();
        }

        if (lifeTime >= delay)
        {
            Elemental elementalInside = influenceArea.Inside.Find((elemental) => ElementsInteractionMatrix.IsAllowed(elemental.Element));

            if (elementalInside != null)
            {
                Explode();
            }
        }
    }

    void OnMouseUpAsButton()
    {
        Explode();
    }


    public void Explode()
    {
        for (int i = influenceArea.Inside.Count - 1; i >= 0; i--)
        {
            influenceArea.Inside[i].GetImpact(element, damage);
        }

        Destroy(gameObject);
    }
}