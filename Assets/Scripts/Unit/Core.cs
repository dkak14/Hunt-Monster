using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core : Unit {
    [SerializeField] SOUnit soUnit = null;
    public override SOUnit SOUnitData => soUnit;
    public void Start() {
        rigid.constraints = RigidbodyConstraints.FreezeAll;
    }
}
