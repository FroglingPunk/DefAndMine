using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableResourcesPackage
{
    [SerializeField] private int money;
    [SerializeField] private int titanium;
    [SerializeField] private int battery;
    [SerializeField] private int chips;
    [SerializeField] private int plasma;

    public Dictionary<EResource, int> All => new Dictionary<EResource, int>
        {
            { EResource.Money, money },
            { EResource.Titanium, titanium },
            { EResource.Battery, battery },
            { EResource.Chips, chips },
            { EResource.Plasma, plasma }
        };
}