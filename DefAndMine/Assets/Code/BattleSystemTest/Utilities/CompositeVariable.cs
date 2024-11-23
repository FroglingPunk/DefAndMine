using System.Collections.Generic;

public abstract class CompositeVariable<T>
{
    protected List<T> components = new List<T>();
    protected List<float> buffs = new List<float>();

    public abstract T Value { get; }


    public void AddComponent(T component)
    {
        components.Add(component);
    }

    public void RemoveComponent(T component)
    {
        components.Remove(component);
    }

    public void AddBuff(float buff)
    {
        buffs.Add(buff);
    }

    public void RemoveBuff(float buff)
    {
        buffs.Remove(buff);
    }
}