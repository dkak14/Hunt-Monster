using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class PlayerHPbar : MonoBehaviour
{
    //[SerializeField] Image BackGroundHpbar;
    [SerializeField] Image Hpbar;
    [SerializeField] Color PlusHpColor;
    [SerializeField] Color MinusHpColor;
    float BarValue = 1;
    float Value = 1;
    void Start()
    {
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.ChangeHp,this, ChangeValue);
    }
    public void ChangeValue(PlayerEvent eventType, Component sender, object param) {
        if(sender is Player) {
            Player player = (Player)sender;
            Value = (float)player.HP / player.SOUnitData.MaxHP;
            Hpbar.DOFillAmount(Value, 1);
        }
    }
}
