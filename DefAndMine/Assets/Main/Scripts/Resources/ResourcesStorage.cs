using System.Collections.Generic;
using UnityEngine;

public class ResourcesStorage
{
    [SerializeField] private Dictionary<EResource, int> Resources { get; set; }

    public int this[EResource resource] => Resources[resource];


    public ResourcesStorage()
    {
        Resources = new Dictionary<EResource, int>();
        for (EResource resourceType = EResource.Money; resourceType <= EResource.Plasma; resourceType++)
        {
            Resources.Add(resourceType, 0);
        }
    }
    public ResourcesStorage(Dictionary<EResource, int> startResources)
    {
        Resources = new Dictionary<EResource, int>(startResources);
        for (EResource resourceType = EResource.Money; resourceType <= EResource.Plasma; resourceType++)
        {
            if (!Resources.ContainsKey(resourceType))
            {
                Resources.Add(resourceType, 0);
            }
        }
    }


    public void Increase(Dictionary<EResource, int> resources)
    {
        foreach (EResource resourceType in resources.Keys)
        {
            Increase(resourceType, resources[resourceType]);
        }
    }

    public void Increase(EResource resourceType, int count)
    {
        Resources[resourceType] += count;
    }

    public bool TryDecrease(Dictionary<EResource, int> resources)
    {
        if (!CanAfford(resources))
        {
            return false;
        }

        foreach (EResource resourceType in resources.Keys)
        {
            Resources[resourceType] -= resources[resourceType];
        }

        return true;
    }

    public bool TryDecrease(EResource resourceType, int count)
    {
        if (!CanAfford(resourceType, count))
        {
            return false;
        }

        Resources[resourceType] -= count;

        return true;
    }

    public bool CanAfford(Dictionary<EResource, int> resources)
    {
        foreach (EResource resourceType in resources.Keys)
        {
            if (!CanAfford(resourceType, resources[resourceType]))
            {
                return false;
            }
        }

        return true;
    }

    public bool CanAfford(EResource resourceType, int count)
    {
        return Resources[resourceType] >= count;
    }


    public override string ToString()
    {
        return "Деньги : " + Resources[EResource.Money] +
            "\nТитан : " + Resources[EResource.Titanium] +
            "\nБатареи : " + Resources[EResource.Battery] +
            "\nЧипы : " + Resources[EResource.Chips] +
            "\nПлазма : " + Resources[EResource.Plasma];
    }
}