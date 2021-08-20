using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] float SensingRange = 3;
    [SerializeField, Range(3, 50)] int SmoothNum = 30;
    RaycastHit rayCast;
    private void Update() {
        Collider[] collider = Physics.OverlapSphere(transform.position, SensingRange, layerMask);
        if (collider.Length > 0) {
            UIManager.Instance.SetActiveShopButton(true);
        }
        else {
            UIManager.Instance.SetActiveShopButton(false);
        }
    }
    private void OnDrawGizmos() {
        MyGizmos.DrawWireCicle(new Vector3(transform.position.x,0, transform.position.z), SensingRange, SmoothNum);
    }
}
