using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Unit
{
    [SerializeField] SOUnit UnitData;
    public override SOUnit SOUnitData => UnitData;

    void Start()
    {
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyWallHealItem, this, HealWall);
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyWallItem, this, BuyWall);
        gameObject.SetActive(false);
    }
    public override void Damaged(int damage) {
        base.Damaged(damage);
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.WallHPChange, this, null);
    }
    void BuyWall(ShopEvent eventType, Component sender, object param) {
        HP = SOUnitData.MaxHP;
        gameObject.SetActive(true);

        transform.position = new Vector3(transform.position.x, 10, transform.position.z);
    }
    void HealWall(ShopEvent eventType, Component sender, object param) {
        hp = UnitData.MaxHP;
    }
}
