using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Weapon : MonoBehaviour
{
    [SerializeField] SOJoyStickValue AttackStickValue;
    [SerializeField] Weapon_Unit weaponData;
    public Weapon_Unit WeaponData => weaponData;

    float ShotCul = 1;
    float shotCul = 0;
    public Bullet bullet; //ÃÑ¾Ë Prefab
    public Transform FirePosition; //ÃÑ¾Ë ¹ß»ç À§Ä¡
    private void Awake() {
        WeaponData.Power = 20;
    }
    private void Update() {
        if(shotCul > 0) {
            shotCul -= Time.deltaTime;
        }
    }
    public void Weapon_Shot()
    {
        if (shotCul <= 0) {
            Debug.Log("ÃÑ¾Ë ¹ß»ç" + WeaponData.Speed);
            Bullet bullet_object = Instantiate(bullet, FirePosition.position, Quaternion.identity);
            bullet_object.transform.eulerAngles = new Vector3(0,transform.eulerAngles.y,0);
            bullet_object.BulletSet(WeaponData);
            bullet_object.GetComponent<Rigidbody>().AddForce(transform.right * WeaponData.Speed);
            WeaponData.Shot = false;
            shotCul = ShotCul;
        }
    }
    public void Weapon_Upgrade(int value)
    {
        WeaponData.Power += value;
    }
}

