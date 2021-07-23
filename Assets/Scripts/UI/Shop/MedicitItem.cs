using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicitItem : ShopItem {
    protected override void BuyItem() {
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.HealPlayer, this, null);
    }

    // 플레이어 HP가 최대가 아니면 구매 가능
    protected override bool BuyItemCondition() {
        List<Unit> PlayerList = UnitManager.Instance.GetSpawnedUnitList("Player");
        if(PlayerList != null) {
            return PlayerList[0].HP == PlayerList[0].SOUnitData.MaxHP ? false : true;
        }
        else {
            Debug.LogWarning("플레이어가 없습니다");
            return false;
        }
    }
}
