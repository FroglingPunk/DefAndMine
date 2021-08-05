using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ElementsInteractionMatrix
{
    [SerializeField] private List<EElement> allowedToAttack;


    public bool IsAllowed(EElement element)
    {
        return allowedToAttack.Contains(element);
    }

    public void Set(EElement element, bool allowed)
    {
        if (allowed)
        {
            if (!allowedToAttack.Contains(element))
            {
                allowedToAttack.Add(element);
            }
        }
        else
        {
            allowedToAttack.Remove(element);
        }
    }

    public int GetPriority(EElement element)
    {
        return allowedToAttack.Contains(element) ? allowedToAttack.Count - allowedToAttack.IndexOf(element) : -1;
    }
}