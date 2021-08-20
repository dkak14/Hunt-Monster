using BT;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections;
using DG.Tweening;
public class Monster : Unit
{
    [SerializeField] SOMonster soMonsterData;
    [SerializeField] Material DieMaterial;
    public override SOUnit SOUnitData => soMonsterData;
    public MonsterAI AI;

    Unit Target;
    public override void Awake() {
        base.Awake();
        AI = GetComponent<MonsterAI>();
        EventManager<MonsterEvent>.Instance.PostEvent(MonsterEvent.Spawn, this, null);
        DieEvent += (Unit unit) => { EventManager<MonsterEvent>.Instance.PostEvent(MonsterEvent.Die, this, null); };
        Stun += () => { AI.StopProcess(); };
        StunEnd += () => { AI.StartProcess();};
    }
    public override void Update() {
        base.Update();
    }
    protected override void OnEnable() {
        base.OnEnable();
        EventManager<MonsterEvent>.Instance.PostEvent(MonsterEvent.Spawn, this, null);
    }
    protected override float DieEffect() {
        GameManager.Instance.Coin += Random.Range(soMonsterData.MinDropGold, soMonsterData.MaxDropGold);
        unitCollider.enabled = false;
        transform.gameObject.layer = LayerMask.NameToLayer("None");
        Debug.Log("»ç¸Á ÀÌÆåÆ®");
        StartCoroutine(C_ChangeAlpha(1));
        return 2.1f;
    }
    IEnumerator C_ChangeAlpha(float duration) {
        MaterialPropertyBlock MPB = new MaterialPropertyBlock();
        float alpha;
        float lastTime = duration;
        if (DieMaterial != null)
            skinMeshRenderer.material = DieMaterial;
        while (lastTime > 0) {
            lastTime -= Time.deltaTime;
            alpha = lastTime / duration;
            MPB.SetFloat("_Mode", 3);
            MPB.SetColor("_Color", new Color(1, 1, 1, alpha));
                skinMeshRenderer.SetPropertyBlock(MPB);
            yield return null;
        }
    }
    protected override bool AttackProcess(Unit target) {
        if (!isStun) {
            Target = target;
            if (animator != null)
                animator.SetBool("attack", true);
            AI.StopProcess();
            StartCoroutine(StartProcess());

        }
        return true;
    }
    IEnumerator StartProcess() {
        yield return new WaitForSeconds(1.5f);
        if (animator != null)
            animator.SetBool("attack", false);
        yield return null;
        if(!isStun)
        AI.StartProcess();
    }
    public void AttackTarget() {
        if (!isStun && isTargetInAttackRange(Target)) {
            Target.Damaged(soMonsterData.AttackDamage);
        }
    }
    bool isTargetInAttackRange(Unit target) {
        Rotate(target.transform);
        Vector2 targetPos = target.GetVec2Position();
        Vector3 dir = (target.transform.position - transform.position).normalized;
        dir = new Vector3(dir.x, 0, dir.z);
        RaycastHit[] hits = Physics.RaycastAll(transform.position, dir, soMonsterData.AttackRange, 1 << target.gameObject.layer);

        bool outRange = true;
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i].transform.gameObject == target.gameObject) {
                outRange = false;
                break;
            }
        }
        // »ç°Å¸® ¹Û
        if (outRange) {
            return false;
        }

        float angle = CalculateAngle(GetVec2Position(), targetPos);
        float transAngle = 360 - transform.eulerAngles.y;
        float angle1 = transAngle - soMonsterData.AttackAngle / 2;
        float angle2 = transAngle + soMonsterData.AttackAngle / 2;
        if (angle2 > 360) {
            angle2 = angle2 - 360;
            if (angle > angle1 && angle > angle2 || angle < angle1 && angle < angle2) {
                return true;
            }
        }
        if(angle1 < 0) {
            angle1 = angle1 + 360;
            if (angle < angle1 && angle < angle2 || angle > angle1 && angle > angle2) {
                return true;
            }
        }
        return angle < angle2 && angle > angle1 ? true : false;
    }
    public float CalculateAngle(Vector2 from , Vector2 to) {
        Vector2 dir = (to - from).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x);
        angle *= Mathf.Rad2Deg;
        if (angle < 0) {
            angle = 180 + (180 + angle);
        }
        return angle;
    }
    private void OnDrawGizmos() {
        Gizmos.color = Color.red;

        float angle1 = -transform.eulerAngles.y + soMonsterData.AttackAngle / 2;
        float angle2 = -transform.eulerAngles.y - soMonsterData.AttackAngle / 2;
        angle1 *= Mathf.Deg2Rad;
        angle2 *= Mathf.Deg2Rad;

        Vector3 dir1 = new Vector3(Mathf.Cos(angle1), 0, Mathf.Sin(angle1));
        Vector3 dir2 = new Vector3(Mathf.Cos(angle2), 0, Mathf.Sin(angle2));

        Gizmos.DrawLine(transform.position, transform.position + dir1 * soMonsterData.AttackRange);
        Gizmos.DrawLine(transform.position, transform.position + dir2 * soMonsterData.AttackRange);
    }
}