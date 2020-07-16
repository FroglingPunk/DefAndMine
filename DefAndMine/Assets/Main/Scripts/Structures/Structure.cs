using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

abstract public class Structure : MonoBehaviour
{
    private Cell[] occupied;
    public Cell[] Occupied
    {
        get
        {
            return occupied;
        }
        set
        {
            if (occupied != null)
            {
                for (int i = 0; i < occupied.Length; i++)
                {
                    occupied[i].Structure = null;
                }
            }

            occupied = value;

            if (occupied != null)
            {
                for (int i = 0; i < occupied.Length; i++)
                {
                    Assert.IsNull(occupied[i].Structure.Value);

                    occupied[i].Structure.Value = this;
                }
            }
        }
    }



    public void Init(Cell[] occupied)
    {
        Occupied = occupied;
    }
}