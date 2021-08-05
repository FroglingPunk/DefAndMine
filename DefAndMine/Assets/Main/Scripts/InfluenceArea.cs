using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(SphereCollider))]
public class InfluenceArea : MonoBehaviour
{
    public List<Elemental> Inside { get; private set; }
    public Elemental GetRandom()
    {
        if (Inside.Count > 0)
        {
            return Inside[UnityEngine.Random.Range(0, Inside.Count)];
        }

        return null;
    }


    public event Action<Elemental> OnEnter;
    public event Action<Elemental> OnExit;


    void Awake()
    {
        Inside = new List<Elemental>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Elemental elemental))
        {
            Inside.Add(elemental);
            elemental.OnDie += OnElementalDie;
            OnEnter?.Invoke(elemental);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Elemental elemental))
        {
            Inside.Remove(elemental);
            elemental.OnDie -= OnElementalDie;
            OnExit?.Invoke(elemental);
        }
    }


    private void OnElementalDie(Elemental elemental)
    {
        elemental.OnDie -= OnElementalDie;
        Inside.Remove(elemental);
        OnExit?.Invoke(elemental);
    }
}