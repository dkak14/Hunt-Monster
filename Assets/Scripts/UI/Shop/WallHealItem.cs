using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallHealItem :ShopItem {

    protected override void BuyItem() {
        EventManager<UIEvnet>.Instance.PostEvent(UIEvnet.BuyWallHealItem, this, null);
    }

    protected override bool BuyItemCondition() {
        Unit wall = UnitManager.Instance.GetSpawnedUnitList("Wall")[0];
        if (wall != null) {
            return wall.HP >= wall.SOUnitData.MaxHP ? false : true;
        }
        else
            return false;
    }
}
