using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] SOPlayer soPlayerData;
    [SerializeField] SOJoyStickValue JoyStickValue;
    [SerializeField] SOJoyStickValue AttackStickValue;
    [SerializeField] Transform tra;
    public override SOUnit SOUnitData => soPlayerData;
    public void Start() {
        EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.Spawn, this, null);
    }
    public void FixedUpdate() {
        if (JoyStickValue.Playing) {
            Move(new Vector3(JoyStickValue.Value.x, 0 , JoyStickValue.Value.y));
        }
    }
    public override void Update() {
        base.Update();
    }
}
