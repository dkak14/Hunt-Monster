using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class ShopItem : MonoBehaviour
{
    [SerializeField] GameObject AbleToPurchase;
    [SerializeField] GameObject UnableToPurchase;
    [SerializeField] int Cost;
    [SerializeField] protected bool isBuyContinue;
    bool isBuyItem = true;
    public Button BuyButton;

    public void Awake() {
        BuyButton.onClick.AddListener(OnClickBuyButton);
    }
    void OnClickBuyButton() {
        if (isBuyItem && BuyItemCondition()) {
            if (Cost >= GameManager.Instance.Gold) {
                BuyItem();
                GameManager.Instance.Gold -= Cost;

                if (!isBuyContinue) {
                    isBuyItem = false;
                    BuyEnd();
                }
            }
        }
    }
    void BuyEnd() {
        AbleToPurchase.SetActive(false);
        UnableToPurchase.SetActive(true);
    }
    /// <summary>
    /// 아이템 구매 조건
    /// </summary>
    /// <returns></returns>
    protected abstract bool BuyItemCondition();
    /// <summary>
    /// 아이템 구매시 일어나는 일
    /// </summary>
    protected abstract void BuyItem();
}
