using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
public class Singleton<T> where T : class
{
    static T instance;
    public static T Instance {
        get { if (instance == null)
                instance = Activator.CreateInstance<T>();
            return instance;
        }

    }
}
public class SingletonBehaviour<T> : MonoBehaviour where T : class {
    public static T instance;
    public static T Instance {
        get
        {
            return instance;
        }
    }
    private void Awake() {
        if (instance == null)
            instance = this.GetComponent<T>();
        else
            Destroy(gameObject);
    }
}

