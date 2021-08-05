using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected EElement element;
    protected float damage;
    protected Elemental target;


    public virtual void Init(Elemental elemental, EElement element, float damage)
    {
        target = elemental;

        this.element = element;
        this.damage = damage;
    }
}