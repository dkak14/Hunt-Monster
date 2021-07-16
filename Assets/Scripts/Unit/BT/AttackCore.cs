using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

namespace BT {
    public class AttackCore : BTNode {
        Monster monster;
        public AttackCore(Monster unit) {
            monster = unit;
        }
        public override bool Invoke() {
            if (GameManager.instance.core != null) {
                float Dst = Vector2.Distance(monster.GetVec2Position(), GameManager.Instance.core.GetVec2Position());
                if (Dst < monster.AttackRange)
                    monster.Attack();
                else {
                    monster.MoveAndRotate(GameManager.Instance.core.transform);
                }
                return true;
            }

            return false;
        }
        public override void DrawGizmos() {
            Gizmos.color = Color.white;
            MyGizmos.DrawWireCicle(monster.transform.position, monster.AttackRange, 30);
        }
    }
}
