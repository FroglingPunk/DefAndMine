using UnityEngine;

public class LinearBullet : Bullet
{
    private float speed = 10f;


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target.GetImpact(element, damage);
            Explode();
        }
    }


    public override void Init(Elemental elemental, EElement element, float damage)
    {
        base.Init(elemental, element, damage);

        target.OnDie += OnTargetDie;
    }

    public void Explode()
    {
        if (target)
        {
            target.OnDie -= OnTargetDie;
        }

        Destroy(gameObject);
    }


    private void OnTargetDie(Elemental dead)
    {
        target.OnDie -= OnTargetDie;

        Explode();
    }
}