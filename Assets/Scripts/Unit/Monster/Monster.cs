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
    private void Start() {
        EventManager<MonsterEvent>.Instance.PostEvent(MonsterEvent.Spawn, this, null);
    }
    protected override void Die() {
        EventManager<MonsterEvent>.Instance.PostEvent(MonsterEvent.Die, this, null);
        GameManager.Instance.Coin += Random.Range(soMonsterData.MinDropGold, soMonsterData.MaxDropGold);
        base.Die();
    }
    protected override bool AttackProcess() {
        Debug.Log("АјАн");
        AI.StopProcess();
        return true;
    }
}