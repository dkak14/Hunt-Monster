using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Unit
{
    [SerializeField] SOUnit UnitData;
    public override SOUnit SOUnitData => UnitData;

    void Start()
    {
        EventManager<UIEvnet>.Instance.AddListener(UIEvnet.BuyWallHealItem, this, HealWall);
        gameObject.SetActive(false);
    }
    public override void Damaged(int damage) {
        base.Damaged(damage);
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.WallHPChange, this, null);
    }
    void HealWall(UIEvnet eventType, Component sender, object param) {
        hp = UnitData.MaxHP;
    }
}
