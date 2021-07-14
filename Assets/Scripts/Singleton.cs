using UnityEngine;
using System;

public class Singleton<T> where T : class, new() {
    public static T Instance {
        get
        {
            if (Singleton<T>._instance == null) {
                Singleton<T>._instance = Activator.CreateInstance<T>();
            }
            return Singleton<T>._instance;
        }
    }
    protected Singleton() {
    }
    private static T _instance;
}
public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance {
        get
        {
            if (SingletonBehaviour<T>.instance == null) {
                SingletonBehaviour<T>.instance = (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
            }
            return SingletonBehaviour<T>.instance;
        }
    }
    private static T instance;
}

public class DontDestroySingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour {
    public static T Instance {
        get
        {
            if (DontDestroySingletonBehaviour<T>.instance == null) {
                DontDestroySingletonBehaviour<T>.instance = (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
            }
            return DontDestroySingletonBehaviour<T>.instance;
        }
    }
    public virtual void Awake() {
        if (instance == null) {
            instance = this.GetComponent<T>();
            DontDestroyOnLoad(this.gameObject);
        }
        if (instance != this)
            Destroy(gameObject);
    }
    private static T instance = null;
}

