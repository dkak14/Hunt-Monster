using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Weapon_Unit WeaponData;
    int Damage;
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    public void BulletSet(Weapon_Unit weaponData) {
        WeaponData = weaponData;
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy") {
            Debug.Log(collision.gameObject.name);
            Debug.Log(collision.gameObject.GetComponent<Unit>());
            collision.gameObject.GetComponent<Unit>().Damaged((int)WeaponData.Power);
            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
