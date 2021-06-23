using System;

public class NotifyingVariable<T>
{
    private T _value;
    public T Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
            OnValueChanged?.Invoke(_value);
        }
    }

    public event Action<T> OnValueChanged;


    public NotifyingVariable() { }
    public NotifyingVariable(T startValue)
    {
        _value = startValue;
    }
}