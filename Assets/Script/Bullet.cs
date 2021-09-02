using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
public class Bullet : MonoBehaviour
{
    Weapon_Unit WeaponData;
    int Damage;
    [Inject] IPlaySound PlaySound;
    private void Awake() {
        Destroy(gameObject, 5f);
    }
    public void BulletSet(Weapon_Unit weaponData) {
        WeaponData = weaponData;
    }
    private void OnTriggerEnter(Collider collision) {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.GetComponent<Unit>().Damaged((int)WeaponData.Power);
            PlaySound.PlayOneShot(SoundType.SFX, "BulletHit");
            Destroy(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}
