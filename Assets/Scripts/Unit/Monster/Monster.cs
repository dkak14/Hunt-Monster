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

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        float angle1 = -transform.eulerAngles.y + soMonsterData.AttackAngle / 2;
        float angle2 = -transform.eulerAngles.y - soMonsterData.AttackAngle / 2;
        angle1 *= Mathf.Deg2Rad;
        angle2 *= Mathf.Deg2Rad;

        Vector3 dir1 = new Vector3(Mathf.Cos(angle1), 0, Mathf.Sin(angle1));
        Vector3 dir2 = new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2));

        Vector3 Vec2Pos = new Vector3(transform.position.x, 0, transform.position.z);
        Gizmos.DrawLine(Vec2Pos, Vec2Pos + dir1 * soMonsterData.AttackRange);
        Gizmos.DrawLine(Vec2Pos, Vec2Pos + dir2 * soMonsterData.AttackRange);
    }
}