using BT;
using UnityEngine;
public class Monster : Unit
{
    public override SOUnit SOUnitData => soMonsterData;

    [SerializeField] protected int minDropGold;
    [SerializeField] protected int maxDropGold;
    [SerializeField] SOMonster soMonsterData = null;
    protected override bool AttackProcess() {
        Debug.Log("АјАн");
        return true;
    }
    protected override void SetUnit(SOUnit unitData) {
        base.SetUnit(unitData);
        attackRange = soMonsterData.AttackRange;
        minDropGold = soMonsterData.MinDropGold;
        maxDropGold = soMonsterData.MaxDropGold;
    }
}