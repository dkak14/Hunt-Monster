using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class Hpbar : MonoBehaviour {
    [SerializeField] Image HpbarImage;
    [SerializeField] Color PlusHpColor;
    [SerializeField] Color MinusHpColor;
    float BarValue = 1;
    float Value = 1;
    public void ChangeValue(Unit unit) {
        Value = (float)unit.HP / unit.SOUnitData.MaxHP;
        HpbarImage.DOFillAmount(Value, 1);
    }
}
