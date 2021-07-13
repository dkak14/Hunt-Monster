using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public abstract SOUnit SOUnitData { get; }
    public int MaxHP => maxHP;
    public int HP => hp;
    public float Speed => speed;

    [SerializeField] int maxHP;
    [SerializeField] int hp;
    [SerializeField] float speed;

    protected Rigidbody rigid;
    public  virtual void Awake() {
        rigid = GetComponent<Rigidbody>();
        SetUnit(SOUnitData);
    }
    #region Rotate
    public void Rotate(float angle) {
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
    public void Rotate(Vector3 target) {
        Vector2 dir = new Vector2(target.x - transform.position.x, target.z - transform.position.z).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, -angle, 0);
    }
    public void Rotate(Transform target) {
        Vector2 dir = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.z - transform.position.z).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, -angle, 0);
    }
    #endregion
    #region Move
    public virtual void Move(Vector3 dir) {
        transform.localPosition += dir * Speed * Time.deltaTime;
    }
    #endregion
    #region Damaged
    public virtual void Damaged(int damage) {
        hp -= damage;
        if (hp <= 0)
            Die();
    }
    #endregion
    #region Attack
    public virtual void Attack() {

    }
    #endregion
    #region Die
    public virtual void Die() {

    }
    #endregion
    // 처음 생성시 유닛의 스텟 설정
    protected virtual void SetUnit(SOUnit unitData) {
        if(unitData == null) {
            Debug.LogWarning("유닛 데이터가 존재하지 않습니다.");
            return;
        }

        maxHP = unitData.MaxHP;
        hp = unitData.MaxHP;
        speed = unitData.Speed;
    }
}
