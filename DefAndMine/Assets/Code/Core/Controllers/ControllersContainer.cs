using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllersContainer
{
    private Dictionary<Type, IController> _controllers = new Dictionary<Type, IController>();


    public T AddController<T>(T controller) where T : IController
    {
        if (_controllers.ContainsKey(typeof(T)))
        {
            Debug.LogError($"Controllers container already contains {typeof(T)} controller");
            return controller;
        }

        _controllers.Add(typeof(T), controller);
        return controller;
    }

    public void AddControllerAs<T, U>(T controller) where T : IController
    {
        if (_controllers.ContainsKey(typeof(U)))
        {
            Debug.LogError($"Controllers container already contains {typeof(T)} controller as {typeof(U)}");
            return;
        }

        _controllers.Add(typeof(U), controller);
    }

    public void AddControllerAs(IController controller, Type type)
    {
        if (_controllers.ContainsKey(type))
        {
            Debug.LogError($"Controllers container already contains {type} controller");
            return;
        }

        _controllers.Add(type, controller);
    }

    public T CreateMonoController<T>() where T : MonoControllerBase
    {
        if (_controllers.ContainsKey(typeof(T)))
            return _controllers[typeof(T)] as T;

        T controller = new GameObject(typeof(T).Name).AddComponent<T>();
        _controllers.Add(typeof(T), controller);
        return controller;
    }

    public T CreateMonoControllerAs<T, U>() where T : MonoControllerBase where U : IController
    {
        if (_controllers.ContainsKey(typeof(U)))
            return _controllers[typeof(U)] as T;

        T controller = new GameObject(typeof(T).Name).AddComponent<T>();
        _controllers.Add(typeof(U), controller);
        return controller;
    }

    public T CreateController<T>() where T : ControllerBase, new()
    {
        if (_controllers.ContainsKey(typeof(T)))
            return _controllers[typeof(T)] as T;

        var controller = new T();
        _controllers.Add(typeof(T), controller);
        return controller;
    }

    public T CreateControllerAs<T, U>() where T : ControllerBase, new()
    {
        if (_controllers.ContainsKey(typeof(U)))
            return _controllers[typeof(U)] as T;

        T controller = new T();
        _controllers.Add(typeof(U), controller);
        return controller;
    }

    public void RemoveController(IController controller)
    {
        if (_controllers.Values.Contains(controller))
        {
            var key = _controllers.FirstOrDefault(x => x.Value == controller).Key;
            _controllers.Remove(key);
        }
        else
        {
            _controllers.Remove(controller.GetType());
        }
    }

    public T GetController<T>() where T : class, IController
    {
        return _controllers.ContainsKey(typeof(T)) ? (T)_controllers[typeof(T)] : null;
    }

    public bool ContainsController<T>()
    {
        return _controllers.ContainsKey(typeof(T));
    }
}