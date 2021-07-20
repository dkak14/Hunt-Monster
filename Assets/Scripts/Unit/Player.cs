using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] SOPlayer soPlayerData;
    [SerializeField] SOJoyStickValue JoyStickValue;
    [SerializeField] SOJoyStickValue AttackStickValue;

    public GameObject bullet; //�Ѿ� Prefab
    public Transform FirePosition; //�Ѿ� �߻� ��ġ
    private bool isShot = false; //�߻� üũ

    public override SOUnit SOUnitData => soPlayerData;
    public void Start() {
        EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.Spawn, this, null);
    }
    public void FixedUpdate() {
        if (JoyStickValue.Playing) {
            Move(new Vector3(JoyStickValue.Value.x, 0 , JoyStickValue.Value.y));
        }
        if (AttackStickValue.Playing) {
            Rotate(new Vector3(AttackStickValue.Value.x, 0, AttackStickValue.Value.y));
            isShot = true;
        }
        if (AttackStickValue.Playing == false && isShot) {
            Shot(bullet, FirePosition);
            isShot = false;
        }
    }
    public override void Update() {
        base.Update();
    }
}
