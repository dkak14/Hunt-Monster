using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform Target;
    [SerializeField] Vector3 pos = Vector3.zero;
    private void Awake() {
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.Spawn, this, SetTarget);
    }
    void SetTarget(PlayerEvent eventType, Component sender, object param) {
        if (sender is Player) {
            Target = sender.transform;
            StartCoroutine(ChasingTarget());
        }
    }
    IEnumerator ChasingTarget() {
        while (true) {
            transform.position = Target.position + pos;
            yield return null;
        }
    }
}
