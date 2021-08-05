using System.Collections.Generic;
using UnityEngine;

public class TowerShooter : Structure, IUnionStructure
{
    [SerializeField] private ElementsInteractionMatrix elementsInteractionMatrix;
    [SerializeField] private EElement element;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private SrtuctureUnions unions = new SrtuctureUnions();

    public override EPlace Place => EPlace.Elevation;
    public ElementsInteractionMatrix ElementsInteractionMatrix => elementsInteractionMatrix;
    public override EElement Element => element;

    private InfluenceArea influenceArea;
    private float timeFromLastShot = 0f;
    private float delayBetweenShots = 1f;
    private float damage = 10;
    private Elemental target;


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
        timeFromLastShot += Time.deltaTime;

        if (timeFromLastShot >= delayBetweenShots)
        {
            if (TryAttack())
            {
                timeFromLastShot = 0f;
            }
        }
    }


    public Structure TryUnion(Structure addStructure)
    {
        return unions.ContainsKey(addStructure) ? unions[addStructure] : null;
    }


    private bool TryAttack()
    {
        if (!influenceArea.Inside.Contains(target))
        {
            target = null;
        }

        if (target == null)
        {
            int maxPriority = int.MinValue;
            Elemental maxPriorityElemental = null;

            for (int i = 0; i < influenceArea.Inside.Count; i++)
            {
                Elemental elementalInside = influenceArea.Inside[i];
                int priority = ElementsInteractionMatrix.GetPriority(elementalInside.Element);

                if (priority >= 0 && priority > maxPriority)
                {
                    maxPriority = priority;
                    maxPriorityElemental = elementalInside;
                }
            }

            target = maxPriorityElemental;
        }

        if (target != null)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity, transform).Init(target, element, damage);
            //target.GetImpact(element, damage);
            return true;
        }

        return false;
    }
}