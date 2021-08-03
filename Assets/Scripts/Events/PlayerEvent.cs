public enum PlayerEvent {
    Spawn, Damaged, Die, ChangeHp
}
public enum MonsterEvent {
    Spawn, Damaged, Die,
}
public enum GameEvent {
    GameStart, CoreSpawn, CoreHPChange, WallHPChange, HealPlayer, WaveStart, WaveEnd
}
public enum UnitEvent {
    Spawn, Die
}
public enum UIEvnet {
    BuyShopItem, BuyWallItem, BuyMEdicitItem, BuyWallHealItem, BuyWeaponUpgradeItem
}