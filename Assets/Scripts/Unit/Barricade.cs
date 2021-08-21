using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Barricade : Unit
{
    [SerializeField] SOUnit UnitData;
    public override SOUnit SOUnitData => UnitData;
    [SerializeField] LayerMask LandingCollision;
    
    void Start() {
        HpValueChangeEvent += (Unit unit) => { EventManager<GameEvent>.Instance.PostEvent(GameEvent.WallHPChange, this, null); };
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyWallHealItem, this, HealWall);
        EventManager<ShopEvent>.Instance.AddListener(ShopEvent.BuyWallItem, this, BuyWall);
        gameObject.SetActive(false);
    }
    void BuyWall(ShopEvent eventType, Component sender, object param) {
        HP = SOUnitData.MaxHP;
        gameObject.SetActive(true);

        transform.position = new Vector3(transform.position.x, 10, transform.position.z);
        unitCollider.enabled = false;
        transform.DOMove(GetCollisionGroundPos(), 0.5f).OnComplete(() => { unitCollider.enabled = true; Landing(); });
    }

    void Landing() {
        RaycastHit[] hits = Physics.BoxCastAll(transform.position, unitCollider.bounds.size, Vector3.up, Quaternion.identity, 10, LandingCollision);
            Debug.Log("È÷µå " + hits.Length);
        StartCoroutine(movePos(hits));
    }
    IEnumerator movePos(RaycastHit[] hits) {
        DOTween.SetTweensCapacity(1000, 1000);
        for (int i = 0; i < hits.Length; i++) {
            Unit hitUnit = hits[i].transform.GetComponent<Unit>();
            LayerMask layer = hits[i].collider.transform.gameObject.layer;
            hits[i].collider.transform.gameObject.layer = LayerMask.NameToLayer("None");

            hitUnit.Stun();
            Vector3 dir = (hits[i].transform.position - transform.position).normalized;
            Vector3 movePos = hits[i].transform.position + dir * 6;
            movePos = new Vector3(movePos.x, hits[i].transform.position.y, movePos.z);

            hits[i].collider.transform.DOKill();
            hits[i].transform.GetComponent<MonoBehaviour>().StopAllCoroutines();
            float Y = hits[i].transform.position.y;
            int k = i;
            hits[k].collider.transform.DOMoveY(Y + 5, 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(() => { hitUnit.gameObject.layer = layer; hitUnit.StunEnd(); });
            hits[i].collider.transform.DOMoveX(movePos.x, 1);
            hits[i].collider.transform.DOMoveZ(movePos.z, 1);
        }
        yield return null;
    }
    protected override float DieEffect() {
        gameObject.SetActive(false);
        return 5;
    }
    void HealWall(ShopEvent eventType, Component sender, object param) {
        hp = UnitData.MaxHP;
    }
}
