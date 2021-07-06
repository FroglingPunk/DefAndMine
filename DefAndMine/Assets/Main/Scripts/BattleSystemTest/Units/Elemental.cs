using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elemental : Battler
{
    [SerializeField] private EElementalType element;


    public EElementalType Element => element;



    public void Morph(EElementalType newElement)
    {

    }
}