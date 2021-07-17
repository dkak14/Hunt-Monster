<<<<<<< HEAD
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

=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
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

/*
public class JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(0, 100)]
    private float leverRange;

    private Vector2 inputDir;
    private bool isInput;


    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData) {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData) {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData) {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
    }

    private void ControlJoystickLever(PointerEventData eventData) {
        var inputPos = rectTransform.position.x > 0 ? eventData.position - rectTransform.anchoredPosition : eventData.position;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDir = inputVector / leverRange;
    }

    private void InputControlVector() {
    }

    void Update() {
        if (isInput)
            InputControlVector();
    }

}
*/
>>>>>>> origin/main
