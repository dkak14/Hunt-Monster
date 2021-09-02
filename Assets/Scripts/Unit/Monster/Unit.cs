using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Zenject;
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
            hp = Mathf.Clamp(hp, 0, SOUnitData.MaxHP);
            if (HpValueChangeEvent != null) {
                HpValueChangeEvent(this);
            }
            if (hp <= 0) DieUnit();

        }
    }
    protected Rigidbody rigid;
    protected MonsterAI monsterAI;
    [SerializeField] public bool Positioning;
    [SerializeField] protected MeshRenderer[] meshRenderer;
    [SerializeField] protected SkinnedMeshRenderer skinMeshRenderer;
    [SerializeField] protected Animator animator;
    public Collider unitCollider;

    public Action<Unit> HpValueChangeEvent;
    public Action<Unit> DieEvent;
    public Action Stun;
    public Action StunEnd;
    protected bool isStun;
    [Inject] MinimapIcon minimapIconObject;
    [Inject] protected ParticleSpawner ParticleSpawn;
    public virtual void Awake() {
        Init();
        if (SOUnitData.MinimapIcon != null) {
            MinimapIcon minimapIcon = Instantiate(minimapIconObject, transform);
            minimapIcon.transform.position = minimapIcon.transform.position + Vector3.up * 50;
            minimapIcon.SettingIcon(SOUnitData.MinimapIcon, SOUnitData.IconSize);
        }
        DieEvent += (Unit unit) => { EventManager<UnitEvent>.Instance.PostEvent(UnitEvent.Die, this, null); };
    }
    protected virtual void Init() {
        TryGetComponent(out rigid);

        if (meshRenderer == null) {
            meshRenderer = transform.GetComponentsInChildren<MeshRenderer>();
            skinMeshRenderer  = transform.GetComponentInChildren<SkinnedMeshRenderer>();
        }
        if (GetComponent<MonsterAI>()) {
            TryGetComponent(out monsterAI);
        }
        SetUnit(SOUnitData);
        TryGetComponent(out unitCollider);

        if (!Positioning) {
            transform.position = GetCollisionGroundPos();
        }

        Stun = null;
        Stun += () => { isStun = true; };

        StunEnd = null;
        StunEnd += () => { isStun = false; };
    }
    public Vector3 GetCollisionGroundPos() {
        if (unitCollider != null) {
            return new Vector3(transform.position.x, unitCollider.bounds.size.y / 2, transform.position.z);
        }
        else
            return new Vector3(transform.position.x, 0, transform.position.z);
    }
    public virtual void Update() {
        rigid.velocity = Vector3.zero;
    }
    protected virtual void OnEnable() {
        if (!Positioning) {
            transform.position = GetCollisionGroundPos();
        }
        EventManager<UnitEvent>.Instance.PostEvent(UnitEvent.Spawn, this, null);
    }
    #region Rotate
    public virtual void Rotate(float angle) {
        transform.DOKill();
        transform.DORotateQuaternion(Quaternion.Euler(0, angle, 0), 1);   
    }
    public void Rotate(Vector3 dir) {
        float angle = Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
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
        Vector3 movePos = transform.position + dir.normalized * (SOUnitData.MoveSpeed * Time.deltaTime);
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
    }
    #endregion
    #region Attack
    // 외부 클래스에서 사용하는 어택 함수 
    // 공격 시 애니메이션 도중에 행동트리를 잠시 중단하고 애니메이션 이벤트에서 다시 실행시킬 부분에서 다시 실행함
    public void Attack(Unit target) {
        if (monsterAI != null && !isStun) {
            //if (AttackProcess())
            //    monsterAI.StopProcess();
            AttackProcess(target);
        }
    }
    // 자식 클래스에서 어떻게 공격할지 구현하는 어택 함수
    protected virtual bool AttackProcess(Unit target) {
        return true;
    }
    #endregion
    #region Die
    public void DieUnit() {
        if (monsterAI != null)
            monsterAI.StopProcess();

        float time = DieEffect();
        if (SOUnitData.DieEffect != null) {
            if(SOUnitData.DieEffect.Length > 0)
            ParticleSpawn.SpawnParticle(SOUnitData.DieEffect, transform.position);
        }
        StartCoroutine(C_Die(time));
    }
    /// <summary>
    /// 죽음 이펙트 연출 함수. 몇 초 뒤에 사망할 지 float을 반환한다
    /// </summary>
    /// <returns></returns>
    protected virtual float DieEffect() {
        return 0;
    }
    IEnumerator C_Die(float time) {
        yield return new WaitForSeconds(time);
        if (DieEvent != null)
            DieEvent(this);
        //Destroy(gameObject);
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
    protected virtual void OnDisable() {
        transform.DOKill();
    }
}
