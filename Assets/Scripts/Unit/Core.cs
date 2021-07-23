using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Unit {
    [SerializeField] SOUnit soUnit = null;
    public override SOUnit SOUnitData => soUnit;
    public override void Damaged(int damage) {
        base.Damaged(damage);
    }
}
