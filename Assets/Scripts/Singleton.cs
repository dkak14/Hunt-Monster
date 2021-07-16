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