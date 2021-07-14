using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;

    void Update()
    {
        Vector3 FixedPos = new Vector3(
            target.transform.position.x + 0f,
            target.transform.position.y + 3.5f,
            target.transform.position.z + -5f);

        transform.position = FixedPos;
    }
}
