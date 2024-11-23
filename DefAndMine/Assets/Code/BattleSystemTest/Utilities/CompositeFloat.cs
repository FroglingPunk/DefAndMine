public class CompositeFloat : CompositeVariable<float>
{
    public override float Value
    {
        get
        {
            float value = 0;
            float buffsMultiply = 0;

            components.ForEach((component) => value += component);
            buffs.ForEach((buff) => buffsMultiply += buff);

            value += (value * buffsMultiply);
            value = UnityEngine.Mathf.Clamp(value, 0, float.MaxValue);

            return value;
        }
    }
}