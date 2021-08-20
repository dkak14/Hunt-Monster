using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallItem : ShopItem
{
    protected override void BuyItem() {
        EventManager<ShopEvent>.Instance.PostEvent(ShopEvent.BuyWallItem, this, null);
        Debug.Log("º® ±¸¸Å");
    }

    protected override bool BuyItemCondition() {
        return true;
    }
}
