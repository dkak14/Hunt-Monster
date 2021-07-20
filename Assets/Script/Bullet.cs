using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 500f;
    [SerializeField] private float timer = 5f;

    private void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
    }
    private void FixedUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
            Destroy(gameObject);
    }
}
