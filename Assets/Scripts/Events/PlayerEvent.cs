public enum PlayerEvent {
    Spawn, Damaged, Die, ChangeHp
}
public enum MonsterEvent {
    Spawn, Damaged, Die,
}
public enum GameEvent {
    GameStart, GameEnd, CoreSpawn, CoreHPChange, WallHPChange, HealPlayer, WaveStart, WaveEnd
}
public enum UnitEvent {
    Spawn, Die
}
public enum ShopEvent {
    BuyShopItem, BuyWallItem, BuyMedicitItem, BuyWallHealItem, BuyWeaponUpgradeItem
}