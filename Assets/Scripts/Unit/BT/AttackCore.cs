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
<<<<<<< HEAD
            if (GameManager.Instance.core != null) {
                float Dst = Vector2.Distance(monster.GetVec2Position(), GameManager.Instance.core.GetVec2Position());
                if (Dst < monster.SOUnitData.AttackRange)
=======
            if (GameManager.instance.core != null) {
                float Dst = Vector2.Distance(monster.GetVec2Position(), GameManager.Instance.core.GetVec2Position());
                if (Dst < monster.AttackRange)
>>>>>>> origin/main
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
<<<<<<< HEAD
            MyGizmos.DrawWireCicle(monster.transform.position, monster.SOUnitData.AttackRange, 30);
=======
            MyGizmos.DrawWireCicle(monster.transform.position, monster.AttackRange, 30);
>>>>>>> origin/main
        }
    }
}
