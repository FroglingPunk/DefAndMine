using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemPolly : MonoBehaviour
{
    public bool DEBUG_FUNC = false;
    public int[] components;


    void OnValidate()
    {
        if(DEBUG_FUNC)
        {
            DEBUG_FUNC = false;
            DebugFunc();
        }
    }

    private void DebugFunc()
    {
        CompositeInt compositeInt = new CompositeInt();
        for(int i = 0; i < components.Length; i++)
        {
            compositeInt.AddComponent(components[i]);
        }
        Debug.Log(compositeInt.Value);
    }
}


public enum EElementalType
{
    Fire,
    Water,
    Earth,
    Air,
    Ice
}

public enum EDamageType
{
    Fire,
    Water,
    Earth,
    Air,
    Ice,
    Physical
}