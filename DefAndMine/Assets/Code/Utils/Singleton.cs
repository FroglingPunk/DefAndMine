using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    static private T instance;
    static public T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();

                if (instance == null)
                {
                    instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                }

                DontDestroyOnLoad(instance);
            }

            return instance;
        }
    }
}