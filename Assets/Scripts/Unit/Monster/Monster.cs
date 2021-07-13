using BT;
using UnityEngine;
public class Monster : Unit
{
    [SerializeField] SOMonster soMonsterData = null;
    public override SOUnit SOUnitData => soMonsterData;

    protected override bool AttackProcess() {
        Debug.Log("АјАн");
        return true;
    }
    protected override void SetUnit(SOUnit unitData) {
        base.SetUnit(unitData);
        attackRange = soMonsterData.AttackRange;
    }
}