using BT;
using UnityEngine;
public class Monster : Unit
{
    [SerializeField] SOMonster soMonsterData = null;
    public override SOUnit SOUnitData => soMonsterData;
    float attackRange;
    public float AttackRange => attackRange;
    protected override void SetUnit(SOUnit unitData) {
        base.SetUnit(unitData);
        attackRange = soMonsterData.AttackRange;
    }
}