using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class Unit : MonoBehaviour
{
    public abstract SOUnit SOUnitData { get; }
    protected int hp;
    public int HP {
        get { return hp; }
        set
        {
            hp = value;
            EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.ChangeHp, this, null);
            if (hp <= 0) Die();
        }
    }
    protected Rigidbody rigid;
    protected MonsterAI monsterAI;
    public  virtual void Awake() {
        rigid = GetComponent<Rigidbody>();
        if (GetComponent<MonsterAI>()) {
            monsterAI = GetComponent<MonsterAI>();
        }
        SetUnit(SOUnitData);
    }
    public virtual void Update() {
        rigid.velocity = Vector3.zero;
    }
    #region Rotate
    public virtual void Rotate(float angle) {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    public void Rotate(Vector3 target) {
        Vector2 dir = new Vector2(target.x - transform.position.x, target.z - transform.position.z).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Rotate(-angle);
    }
    public void Rotate(Transform target) {
        Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Rotate(-angle);
    }
    #endregion
    #region Move
    public virtual void Move(Vector3 dir) {
        Vector3 movePos = transform.position + dir.normalized * (SOUnitData.Speed * Time.deltaTime);
        rigid.MovePosition(movePos); // rigidbody.MovePosition 이 Transfome.position을 이용한 것보다 더 성능이 좋다
    }
    public void Move(float angle) {
        Vector3 dir = new Vector3(Mathf.Cos(angle),0, Mathf.Sin(angle)) * Mathf.Deg2Rad;
        Move(dir);
    }
    public void Move(Transform Target) {
        Vector3 dir = new Vector3(Target.position.x - transform.position.x,0, Target.position.z - transform.position.z).normalized;
        Move(dir);
    }
    #endregion
    #region MoveAndRotate
    public void MoveAndRotate(float angle) {
        Move(angle);
        Rotate(angle);
    }
    public void MoveAndRotate(Vector3 dir) {
        Move(dir);
        Rotate(dir);
    }
    public void MoveAndRotate(Transform Target) {
        Move(Target);
        Rotate(Target);
    }
    #endregion
    #region Damaged
    public virtual void Damaged(int damage) {
        HP -= damage;
        Debug.Log(hp);
    }
    #endregion
    #region Attack
    // 외부 클래스에서 사용하는 어택 함수 
    // 공격 시 애니메이션 도중에 행동트리를 잠시 중단하고 애니메이션 이벤트에서 다시 실행시킬 부분에서 다시 실행함
    public void Attack() {
        if (monsterAI != null) {
            //if (AttackProcess())
            //    monsterAI.StopProcess();
            AttackProcess();
        }
    }
    // 자식 클래스에서 어떻게 공격할지 구현하는 어택 함수
    protected virtual bool AttackProcess() {
        return true;
    }
    #endregion
    #region Die
    public virtual void Die() {

    }
    #endregion
    public Vector2 GetVec2Position() {
        return new Vector2(transform.position.x, transform.position.z);
    }
    // 처음 생성시 유닛의 스텟 설정
    protected virtual void SetUnit(SOUnit unitData) {
        if(unitData == null) {
            Debug.LogWarning("유닛 데이터가 존재하지 않습니다.");
            return;
        }
        hp = unitData.MaxHP;
    }
}
