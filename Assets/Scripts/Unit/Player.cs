using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Zenject;
using DG.Tweening;
public class Player : Unit
{
    [SerializeField] SOPlayer soPlayerData;
    [SerializeField] SOJoyStickValue JoyStickValue;
    [SerializeField] SOJoyStickValue AttackStickValue;
    [SerializeField] Material InvincivilityMaterial;
    Character_Weapon playerWeapon;
    public override SOUnit SOUnitData => soPlayerData;
    [SerializeField] PlayerLeg[] Legs;
    bool Invincivility = false;
    bool GameEnd;

    [Inject] IPlaySound PlaySound;
    public override void Awake() {
        base.Awake();
        playerWeapon = GetComponent<Character_Weapon>();
        playerWeapon.ShotBulet += () => { animator.SetTrigger("attack"); };

        for(int i = 0;i< Legs.Length; i++) {
            Legs[i].vec = Legs[i].Leg.rotation.eulerAngles;
        }
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyMedicitItem, this, (eventType, sender, param) => { if(hp > 0)HP = soPlayerData.MaxHP; });
        EventManager<GameEvent>.Instance.AddListener(GameEvent.GameEnd, this, (eventType, sender, param) => { GameEnd = true; });
        HpValueChangeEvent += (unit) => { EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.ChangeHp, this, null); };
    }
    public void Start() {
        EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.Spawn, this, null);
    }
    public override void Rotate(float angle) {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    protected override float DieEffect() {
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.GameEnd, this, null);
        //EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.Die, this, null);
        return 1;
    }
    public override void Damaged(int damage) {
        if (!Invincivility) {
            base.Damaged(damage);
            StartCoroutine(C_Invincibility(soPlayerData.InvincibilityTime));
        }
    }
    IEnumerator C_Invincibility(float time) {
        Invincivility = true;
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetColor("_Color", new Color(1, 1, 1, 0.5f));

        Material meterial = skinMeshRenderer.material;
        skinMeshRenderer.material = InvincivilityMaterial;
        for (int i = 0; i < meshRenderer.Length; i++)
            meshRenderer[i].SetPropertyBlock(block);
        yield return new WaitForSeconds(time);
        Invincivility = false;
        block.SetColor("_Color", new Color(1, 1, 1, 1));
        for (int i = 0; i < meshRenderer.Length; i++)
            meshRenderer[i].SetPropertyBlock(block);
        skinMeshRenderer.material = meterial;
    }

    public void FixedUpdate() {
        if (hp > 0) {
            if (JoyStickValue.Playing && !GameEnd) {
                Vector3 dir = new Vector3(JoyStickValue.Value.x, 0, JoyStickValue.Value.y);
                Move(dir);
                if (!AttackStickValue.Playing) {
                    Rotate(new Vector3(JoyStickValue.Value.x, 0, JoyStickValue.Value.y));
                }
                animator.SetBool("move", true);
            }
            else {
                animator.SetBool("move", false);
            }
            if (AttackStickValue.Playing) {
                Rotate(new Vector3(AttackStickValue.Value.x, 0, AttackStickValue.Value.y));
                playerWeapon.Weapon_Shot();
            }
        }
    }
    public override void Update() {
        base.Update();
        if (Input.GetKeyDown(KeyCode.K))
            EventManager<ShopEvent>.Instance.PostEvent(ShopEvent.BuyWallItem, this, null);
        if (Input.GetKeyDown(KeyCode.P))
            EventManager<GameEvent>.Instance.PostEvent(GameEvent.GameEnd, this, null);
    }
    private void LateUpdate() {
        if (AttackStickValue.Playing) {
            if (JoyStickValue.Playing) {
                float moveAngle = Mathf.Atan2(JoyStickValue.Value.y, JoyStickValue.Value.x);
                float attackAngle = Mathf.Atan2(AttackStickValue.Value.y, AttackStickValue.Value.x);
                moveAngle *= Mathf.Rad2Deg;
                attackAngle *= Mathf.Rad2Deg;

                float angleDifference = Mathf.Abs(attackAngle - moveAngle);
                animator.SetFloat("angleX", JoyStickValue.Value.x);
                animator.SetFloat("angleY", JoyStickValue.Value.y);

                //if(angleDifference > 180) {
                //    angleDifference = 180 -( angleDifference - 180);
                //}

                //if (angleDifference < 50) {
                //    for (int i = 0; i < Legs.Length; i++) {
                //        float angle = 0;
                //        if (moveAngle <= 90) {
                //            angle = 90 + 90 - moveAngle;
                //        }
                //        else if (90 <= moveAngle && moveAngle <= 180) {
                //            angle = 180 - moveAngle;
                //        }
                //        else if (180 <= moveAngle && moveAngle <= 270) {
                //            angle = 360 - moveAngle - 180;
                //        }
                //        else if (270 <= moveAngle && moveAngle <= 360) {
                //            angle = 180 + 360 - moveAngle;
                //        }
                //        Vector3 rotate = Legs[i].Leg.transform.eulerAngles;
                //        float totalAngle = angle;
                //        Legs[i].Leg.transform.rotation = Quaternion.Euler(rotate.x, totalAngle, rotate.z);
                //    }
                //}
            }
        }
    }

    [Serializable]
    public class PlayerLeg {
        public Transform Leg;
        public Vector3 vec;
    }
}