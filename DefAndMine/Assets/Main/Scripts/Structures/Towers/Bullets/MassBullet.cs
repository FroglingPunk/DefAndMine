using System.Collections;
using UnityEngine;

public class MassBullet : Bullet
{
    private float speed = 5f;
    private Vector3 targetLastPosition;
    private Coroutine explosionCoroutine = null;


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetLastPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetLastPosition) < 0.1f)
        {
            Explode();
        }
    }


    public override void Init(Elemental elemental, EElement element, float damage)
    {
        base.Init(elemental, element, damage);

        targetLastPosition = target.transform.position;
    }

    public void Explode()
    {
        if (explosionCoroutine == null)
        {
            explosionCoroutine = StartCoroutine(ExplosionCoroutine());
        }
    }

    private IEnumerator ExplosionCoroutine()
    {
        Transform influenceAreaTransform = new GameObject("InfluenceArea", typeof(InfluenceArea)).transform;

        influenceAreaTransform.parent = transform;
        influenceAreaTransform.localPosition = Vector3.zero;

        influenceAreaTransform.GetComponent<Collider>().isTrigger = true;
        influenceAreaTransform.GetComponent<SphereCollider>().radius = 1f;

        yield return null;

        InfluenceArea influenceArea = influenceAreaTransform.GetComponent<InfluenceArea>();
        for (int i = influenceArea.Inside.Count - 1; i >= 0; i--)
        {
            influenceArea.Inside[i].GetImpact(element, damage);
        }

        Destroy(gameObject);
    }
}