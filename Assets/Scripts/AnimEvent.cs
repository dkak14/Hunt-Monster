using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{
    public UnityEvent UnitEvent;
    public void Invoke() {
        UnitEvent.Invoke();
    }
}
