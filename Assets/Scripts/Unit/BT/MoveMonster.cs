using BT;
using UnityEngine;

namespace BT {
    public class MoveMonster : BTNode {
        Monster monster;
        public MoveMonster(Monster monster) {
            this.monster = monster;
        }
        public override bool Invoke() {
            monster.Move(Vector3.right);
            return true;
        }
    }
}