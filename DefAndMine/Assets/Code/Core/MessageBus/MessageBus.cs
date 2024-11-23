using System;
using System.Collections.Generic;

public class MessageBus : ControllerBase
{
    private Dictionary<Type, Delegate> _callbacks = new Dictionary<Type, Delegate>();


    public void Callback<T>(T message) where T : IMessage
    {
        if (!_callbacks.ContainsKey(message.GetType()))
        {
            return;
        }

        var callback = (Action<T>)_callbacks[typeof(T)];
        callback?.Invoke(message);
    }

    public void Subscribe<T>(Action<T> callback) where T : IMessage
    {
        if (!_callbacks.ContainsKey(typeof(T)))
        {
            _callbacks.Add(typeof(T), callback);
        }
        else
        {
            _callbacks[typeof(T)] = Delegate.Combine(_callbacks[typeof(T)], callback);
        }
    }

    public void Unsubscribe<T>(Action<T> callback) where T : IMessage
    {
        if (!_callbacks.ContainsKey(typeof(T)))
        {
            return;
        }

        _callbacks[typeof(T)] = Delegate.Remove(_callbacks[typeof(T)], callback);
    }
}