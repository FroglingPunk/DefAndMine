using System.Collections.Generic;
using UnityEngine;

public class ChainBullet : Bullet
{
    private float speed = 5f;
    private int maxTargetsCount = 4;
    private float maxDistanceBetweenTargets = 4f;
    private List<Elemental> hittedTargets = new List<Elemental>();


    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            hittedTargets.Add(target);
            target.GetImpact(element, damage);

            if (hittedTargets.Count < maxTargetsCount)
            {
                NextTarget();
            }
            else
            {
                Explode();
            }
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


    private void NextTarget()
    {
        if (target)
        {
            target.OnDie -= OnTargetDie;
        }

        Elemental[] allElementals = FindObjectsOfType<Elemental>();
        float minDistanceToHittedElemental = float.MaxValue;
        float minDistanceToNonHittedElemental = float.MaxValue;
        Elemental closestHittedElemental = null;
        Elemental closestNonHittedElemental = null;

        for (int i = 0; i < allElementals.Length; i++)
        {
            Elemental elemental = allElementals[i];

            if (target && elemental == target)
            {
                continue;
            }
            else
            {
                float distance = Vector3.Distance(transform.position, elemental.transform.position);

                if (distance <= maxDistanceBetweenTargets)
                {
                    if (hittedTargets.Contains(elemental))
                    {
                        if (distance < minDistanceToHittedElemental)
                        {
                            minDistanceToHittedElemental = distance;
                            closestHittedElemental = elemental;
                        }
                    }
                    else
                    {
                        if (distance < minDistanceToNonHittedElemental)
                        {
                            minDistanceToNonHittedElemental = distance;
                            closestNonHittedElemental = elemental;
                        }
                    }
                }
            }
        }

        if (closestNonHittedElemental)
        {
            target = closestNonHittedElemental;
            target.OnDie += OnTargetDie;
        }
        else if (closestHittedElemental)
        {
            target = closestHittedElemental;
            target.OnDie += OnTargetDie;
        }
        else
        {
            Explode();
        }
    }

    private void OnTargetDie(Elemental dead)
    {
        target.OnDie -= OnTargetDie;

        Explode();
    }
}