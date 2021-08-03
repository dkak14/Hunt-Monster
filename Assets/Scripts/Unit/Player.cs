using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] SOPlayer soPlayerData;
    [SerializeField] SOJoyStickValue JoyStickValue;
    [SerializeField] SOJoyStickValue AttackStickValue;
    Character_Weapon playerWeapon;
    public override SOUnit SOUnitData => soPlayerData;
    public override void Awake() {
        base.Awake();
        playerWeapon = GetComponent<Character_Weapon>();
    }
    public void Start() {
        EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.Spawn, this, null);
    }
    public override void Rotate(float angle) {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    public override void Damaged(int damage) {
        base.Damaged(damage);
        EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.ChangeHp, this, null);
    }
    public void FixedUpdate() {
        if (JoyStickValue.Playing) {
            Vector3 dir = new Vector3(JoyStickValue.Value.x, 0, JoyStickValue.Value.y);
            Move(dir);
            if (!AttackStickValue.Playing) {
                Rotate(new Vector3(JoyStickValue.Value.x, 0, JoyStickValue.Value.y));
            }
        }
        if (AttackStickValue.Playing) {
            Rotate(new Vector3(AttackStickValue.Value.x, 0, AttackStickValue.Value.y));
            playerWeapon.Weapon_Shot();
        }
    }
    public override void Update() {
        base.Update();
    }
}