using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
public class AnimEvent : MonoBehaviour
{
    public UnityEvent UnitEvent;

    public void Invoke() {
        UnitEvent.Invoke();
    }
}
