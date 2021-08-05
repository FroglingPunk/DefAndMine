using UnityEngine;

public abstract class Structure : MonoBehaviour
{
    public abstract EPlace Place { get; }
    public abstract EElement Element { get; }
}