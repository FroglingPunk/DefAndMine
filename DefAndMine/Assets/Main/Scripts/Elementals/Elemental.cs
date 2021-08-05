using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Elemental : MonoBehaviour
{
    [SerializeField] private ElementalBehaviour behaviour;
    [SerializeField] private EElement element;
    [SerializeField] private float maxHealth = 15;

    private NavMeshAgent navMeshAgent;
    private Vector3[] wayPoints;
    private Vector3 currentWayPoint;
    private int currentWayPointID;

    public float MaxHealth => maxHealth;
    private float health = 15;

    private MorphControl morphControl;

    public EElement Element => element;

    public event Action<Elemental> OnDie;
    public event Action<EElement, float> OnGetImpact;


    void Awake()
    {
        health = MaxHealth;
        navMeshAgent = GetComponent<NavMeshAgent>();
        morphControl = new MorphControl(this);
    }

    void Update()
    {
        if (currentWayPointID < wayPoints.Length)
        {
            Vector2 position = new Vector2(transform.position.x, transform.position.z);
            Vector2 wp2D = new Vector2(currentWayPoint.x, currentWayPoint.z);

            if (Vector2.Distance(position, wp2D) < 0.1f)
            {
                currentWayPointID++;
                if (currentWayPointID < wayPoints.Length)
                {
                    currentWayPoint = wayPoints[currentWayPointID];
                    navMeshAgent.SetDestination(currentWayPoint);
                }
                else
                {
                    navMeshAgent.isStopped = true;
                }
            }
        }
    }


    public void Init(Elemental elemental)
    {
        wayPoints = elemental.wayPoints;
        currentWayPoint = elemental.currentWayPoint;
        currentWayPointID = elemental.currentWayPointID;

        navMeshAgent.SetDestination(currentWayPoint);
    }

    public void Init(Vector3[] movePoints)
    {
        wayPoints = movePoints;
        currentWayPoint = movePoints[0];
        currentWayPointID = 0;

        navMeshAgent.SetDestination(currentWayPoint);
    }

    public void GetImpact(EElement element, float value)
    {
        OnGetImpact?.Invoke(element, value);
        behaviour.React(this, element, value);
    }

    public void GetDamage(float damage)
    {
        if (health > damage)
        {
            health -= damage;
        }
        else
        {
            Die();
        }
    }

    public void Heal(float heal)
    {
        health = Mathf.Clamp(health + heal, 0, MaxHealth);
    }

    public void Morph(EElement element, float value)
    {
        morphControl.Add(element, value);
    }

    public void Morph(EElement morphElement)
    {
        element = morphElement;
        morphControl.Clear();
        Morpher.Instance.UpdateMorph(this);
        behaviour = PrefabsArchive.Instance.GetElementalBehaviour(morphElement);
    }

    public void Die()
    {
        health = 0;
        OnDie?.Invoke(this);
        Destroy(gameObject);
    }


    public static Elemental Unite(Elemental elementalA, Elemental elementalB)
    {
        EElement unionResult = EElement.COUNT;
        if (elementalA.behaviour.IsUnites(elementalB.element, ref unionResult))
        {
            Elemental result = elementalA;
            result.Morph(unionResult);
            result.Heal(result.MaxHealth);
            result.morphControl.Clear();

            elementalB.Die();

            return result;
        }
        else if (elementalB.behaviour.IsUnites(elementalA.element, ref unionResult))
        {
            Elemental result = elementalB;
            result.Morph(unionResult);
            result.Heal(result.MaxHealth);
            result.morphControl.Clear();

            elementalA.Die();

            return result;
        }
        else
        {
            return null;
        }
    }
}