public class CompositeInt : CompositeVariable<int>
{
    public override int Value
    {
        get
        {
            int value = 0;
            float buffsMultiply = 0;

            components.ForEach((component) => value += component);
            buffs.ForEach((buff) => buffsMultiply += buff);

            value += (int)(value * buffsMultiply);
            value = UnityEngine.Mathf.Clamp(value, 0, int.MaxValue);

            return value;
        }
    }
}