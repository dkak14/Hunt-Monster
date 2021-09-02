using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{
   [SerializeField] bool isActiveCollider;
   [SerializeField] MeshRenderer[] meshRenderer;
   [SerializeField] Collider ObjectCollider;
    public void SetActiveRenderer(bool active) {
        for(int i = 0; i < meshRenderer.Length; i++) {
            meshRenderer[i].enabled = active;
        }
        if (isActiveCollider)
            ObjectCollider.enabled = active;
        else
            ObjectCollider.enabled = false;
    }
}
