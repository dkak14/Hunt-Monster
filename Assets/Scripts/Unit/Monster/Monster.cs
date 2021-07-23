using BT;
using UnityEngine;
public class Monster : Unit
{
    [SerializeField] SOMonster soMonsterData;
    public override SOUnit SOUnitData => soMonsterData;
    protected MonsterAI AI;
    public override void Awake() {
        base.Awake();
        AI = GetComponent<MonsterAI>();
    }
    protected override bool AttackProcess() {
        Debug.Log("АјАн");
        AI.StopProcess();
        return true;
    }
}