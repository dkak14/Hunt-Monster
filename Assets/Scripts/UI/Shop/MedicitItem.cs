using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedicitItem : ShopItem {
    protected override void BuyItem() {
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.HealPlayer, this, null);
    }

    // �÷��̾� HP�� �ִ밡 �ƴϸ� ���� ����
    protected override bool BuyItemCondition() {
        List<Unit> PlayerList = UnitManager.Instance.GetSpawnedUnitList("Player");
        if(PlayerList != null) {
            return PlayerList[0].HP == PlayerList[0].SOUnitData.MaxHP ? false : true;
        }
        else {
            Debug.LogWarning("�÷��̾ �����ϴ�");
            return false;
        }
    }
}
