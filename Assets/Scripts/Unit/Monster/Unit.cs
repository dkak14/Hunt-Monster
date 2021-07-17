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
        rigid.MovePosition(movePos); // rigidbody.MovePosition �� Transfome.position�� �̿��� �ͺ��� �� ������ ����
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
    // �ܺ� Ŭ�������� ����ϴ� ���� �Լ� 
    // ���� �� �ִϸ��̼� ���߿� �ൿƮ���� ��� �ߴ��ϰ� �ִϸ��̼� �̺�Ʈ���� �ٽ� �����ų �κп��� �ٽ� ������
    public void Attack() {
        if (monsterAI != null) {
            //if (AttackProcess())
            //    monsterAI.StopProcess();
            AttackProcess();
        }
    }
    // �ڽ� Ŭ�������� ��� �������� �����ϴ� ���� �Լ�
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
    // ó�� ������ ������ ���� ����
    protected virtual void SetUnit(SOUnit unitData) {
        if(unitData == null) {
            Debug.LogWarning("���� �����Ͱ� �������� �ʽ��ϴ�.");
            return;
        }
        hp = unitData.MaxHP;
    }
}
