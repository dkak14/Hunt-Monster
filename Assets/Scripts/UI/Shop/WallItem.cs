using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallItem : ShopItem
{
    protected override void BuyItem() {
        Unit wall = UnitManager.Instance.GetSpawnedUnitList("Wall")[0];
        wall.HP = wall.SOUnitData.MaxHP;
        wall.gameObject.SetActive(true);
        Debug.Log("º® ±¸¸Å");
    }

    protected override bool BuyItemCondition() {
        return true;
    }
}
