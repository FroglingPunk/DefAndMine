using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsInteraction : MonoBehaviour
{
    public EElement elemental;
    public EElement impact;
    public Result[] results;
}

[System.Serializable]
public class Result
{
    public EElementsInteraction kind;
    public EElement morphOutcome;
    public float multiply;
}

public enum EElementsInteraction
{
    Damage,
    Heal,
    Morph
}