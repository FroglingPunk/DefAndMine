using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Unit : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private int maxHealth = 30;

    private int health;
    public int Health
    {
        get => health;
        set
        {
            health = value;

            OnHealthChanged?.Invoke(this);

            if (health == 0)
            {
                OnDie?.Invoke(this);
            }
        }
    }

    public event Action<Unit> OnHealthChanged;
    public event Action<Unit> OnDie;


    public void Init(Transform destination)
    {
        health = maxHealth;
        navMeshAgent.SetDestination(destination.position);
    }

    public void Die()
    {
        Health = 0;

        Destroy(gameObject);
    }

    public void GetDamage(int damage)
    {
        if (Health > damage)
        {
            Health -= damage;
        }
        else
        {
            Die();
        }
    }
}