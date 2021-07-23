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
    /// ������ ���� ����
    /// </summary>
    /// <returns></returns>
    protected abstract bool BuyItemCondition();
    /// <summary>
    /// ������ ���Ž� �Ͼ�� ��
    /// </summary>
    protected abstract void BuyItem();
}
