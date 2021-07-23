using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameEvent {
    GameStart, CoreSpawn, CoreHPChange, WallHPChange, HealPlayer
}
public enum UnitEvent {
    Spawn, Die
}
public enum UIEvnet {
    BuyShopItem, BuyWallItem,BuyMEdicitItem, BuyWallHealItem, BuyWeaponUpgradeItem
}
public class GameManager : SingletonBehaviour<GameManager>
{
    int gold = 0;
    public int Gold { get { return gold; }set { gold = value; gold = Mathf.Max(gold, 0); } }
    public Core core;
    public void Start() {
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.CoreSpawn, core, null);
    }
}
