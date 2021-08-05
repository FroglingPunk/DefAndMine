using System.Collections.Generic;
using UnityEngine;

public class MorphControl
{
    private Elemental elemental;
    private Dictionary<EElement, float> progress = new Dictionary<EElement, float>();
    private float limit;


    public MorphControl(Elemental elemental)
    {
        this.elemental = elemental;
        limit = elemental.MaxHealth;
    }

    public void Clear()
    {
        progress.Clear();
    }

    public void Add(EElement element, float value)
    {
        if (!progress.ContainsKey(element))
        {
            progress.Add(element, 0);
        }

        progress[element] = Mathf.Clamp(progress[element] + value, 0, limit);

        if (progress[element] == limit)
        {
            elemental.Morph(element);
        }
    }
}