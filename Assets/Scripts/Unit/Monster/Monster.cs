using BT;
using UnityEngine;
public class Monster : Unit
{
<<<<<<< HEAD
    [SerializeField] SOMonster soMonsterData;
    public override SOUnit SOUnitData => soMonsterData;

=======
    public override SOUnit SOUnitData => soMonsterData;

    [SerializeField] protected int minDropGold;
    [SerializeField] protected int maxDropGold;
    [SerializeField] SOMonster soMonsterData = null;
>>>>>>> origin/main
    protected override bool AttackProcess() {
        Debug.Log("АјАн");
        return true;
    }
<<<<<<< HEAD
=======
    protected override void SetUnit(SOUnit unitData) {
        base.SetUnit(unitData);
        attackRange = soMonsterData.AttackRange;
        minDropGold = soMonsterData.MinDropGold;
        maxDropGold = soMonsterData.MaxDropGold;
    }
>>>>>>> origin/main
}