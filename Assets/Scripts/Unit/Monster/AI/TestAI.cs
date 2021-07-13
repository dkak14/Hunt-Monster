using BT;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAI : MonsterAI
{
    BTSelector selector = new BTSelector();
    BTSelector selector2 = new BTSelector();

    AttackTarget AttackPlayer;
    DebugMonster BTDebug;
    protected override void InitAI(BTSequence root) {
        root.AddChild(selector);
        root.AddChild(selector2);

        AttackPlayer = new AttackTarget(monster, 10, 5, 1 << LayerMask.NameToLayer("Player"));
        BTDebug = new DebugMonster();

        selector.AddChild(AttackPlayer);
        selector.AddChild(BTDebug);
    }
}
