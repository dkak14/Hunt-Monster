using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitHpbar : Hpbar
{
    [SerializeField] Unit unit;
    [SerializeField] Vector2 OffSet = Vector3.zero;
    private void Start() {
        HpbarSetting(unit, OffSet);
    }
    private void Update() {
        HpbarImage.rectTransform.position = Camera.main.WorldToScreenPoint(unit.transform.position) + (Vector3)OffSet;
    }
    public void HpbarSetting(Unit unit, Vector2 offSet) {
        this.unit = unit;
        unit.HpValueChangeEvent += ChangeValue;
        OffSet = offSet;
        ChangeValue(unit);
    }
}
