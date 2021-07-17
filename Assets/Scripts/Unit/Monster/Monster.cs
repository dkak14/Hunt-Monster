using BT;
using UnityEngine;
public class Monster : Unit
{
    [SerializeField] SOMonster soMonsterData;
    public override SOUnit SOUnitData => soMonsterData;
    protected override bool AttackProcess() {
        Debug.Log("АјАн");
        return true;
    }
}