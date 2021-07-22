using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Weapon : MonoBehaviour
{
    [SerializeField] SOJoyStickValue AttackStickValue;
    [SerializeField] Weapon_Unit WeaponData;

    public GameObject bullet; //�Ѿ� Prefab
    public Transform FirePosition; //�Ѿ� �߻� ��ġ

    public void FixedUpdate()
    {
        if (AttackStickValue.Playing == false && WeaponData.Shot){
            Weapon_Shot();
        }
    }
    public void Weapon_Shot()
    {
       var bullet_object = Instantiate(bullet, FirePosition.position, FirePosition.rotation);
       bullet_object.GetComponent<Rigidbody>().AddForce(transform.forward * WeaponData.Speed);
       WeaponData.Shot = false;
    }
    public void Weapon_Upgrade()
    {
        WeaponData.Power += 10;
    }
}

