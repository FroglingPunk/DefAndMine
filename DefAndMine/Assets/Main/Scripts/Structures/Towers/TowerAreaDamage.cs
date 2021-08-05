using UnityEngine;

public class TowerAreaDamage : Structure
{
    [SerializeField] private EPlace place = EPlace.Elevation;
    [SerializeField] private EElement element;

    public override EPlace Place => place;
    public override EElement Element => element;

    private InfluenceArea influenceArea;
    private float timeFromLastAttack = 0f;
    private int damagePerSecond = 5;


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

        if (timeFromLastAttack >= 1f)
        {
            Attack();
            timeFromLastAttack = 0f;
        }
    }


    private void Attack()
    {
        for (int i = influenceArea.Inside.Count - 1; i >= 0; i--)
        {
            influenceArea.Inside[i].GetImpact(element, damagePerSecond);
        }
    }
}