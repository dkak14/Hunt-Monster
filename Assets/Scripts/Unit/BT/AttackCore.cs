using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BT;

namespace BT {
    public class AttackCore : BTNode {
        Monster monster;
        float UpdateCul = 1;
        float updateCul = 1;
        Node[] Way;
        int CurrentWayIndex;
        int layerMask = (1 << LayerMask.NameToLayer("Object")) + (1 << LayerMask.NameToLayer("Wall"));
        float DirectCoreCul = 2;
        float directCoreCul = 0;

        bool FollowingPath = true;
        public AttackCore(Monster unit) {
            monster = unit;
        }
        public override bool Invoke() {
            FollowingPath = false;
            if (GameManager.Instance.core != null) {
                if (Way == null) {
                    Way = Pathfinding.FindPath(monster.transform, GameManager.Instance.core.transform);
                    CurrentWayIndex = 0;
                }
                
                float Dst = Vector2.Distance(monster.GetVec2Position(), GameManager.Instance.core.GetVec2Position());
                if (Dst < monster.SOUnitData.AttackRange)
                    monster.Attack();
                else {
                    if (CheckWall()) {
                        return true;
                        
                    }
                    if (updateCul >= 0) {
                        updateCul -= Time.deltaTime;
                    }
                    else {
                        updateCul = UpdateCul;
                        Way = Pathfinding.FindPath(monster.transform, GameManager.Instance.core.transform);
                        CurrentWayIndex = 1;
                    }
                    FollowPath();
                }
                return true;
            }

            return false;
        }
        bool CheckWall() {
            Vector3 dirToCore = (GameManager.Instance.core.transform.position - monster.transform.position).normalized;
            float dst = Vector2.Distance(monster.GetVec2Position(), GameManager.Instance.core.GetVec2Position());
            RaycastHit ray;
            if (!Physics.Raycast(monster.transform.position, dirToCore, out ray, dst, layerMask)) {
                if (directCoreCul <= 0) {
                    monster.MoveAndRotate(GameManager.Instance.core.transform);
                    return true;
                }
                else {
                    directCoreCul -= Time.deltaTime;
                    return false;
                }
            }
            else {
                if (ray.collider.tag == "Wall") {
                    FollowingPath = false;
                    monster.MoveAndRotate(ray.collider.transform);
                    return true;
                }
            }
            directCoreCul = DirectCoreCul;
            return false;
        }
        public void FollowPath() {
            FollowingPath = true;
            if (CurrentWayIndex < Way.Length) {
                float Dst = Vector2.Distance(Way[CurrentWayIndex].WorldVec2Position, monster.GetVec2Position());
                if (Dst < 0.5f) {
                    CurrentWayIndex++;
                }
                Vector2 vec2Dir = (Way[CurrentWayIndex].WorldVec2Position - monster.GetVec2Position() ).normalized;
                Vector3 dir = new Vector3(vec2Dir.x,0, vec2Dir.y);
                monster.MoveAndRotate(dir);
            }
        }
        public override void DrawGizmos() {
            Gizmos.color = Color.white;
            MyGizmos.DrawWireCicle(monster.transform.position, monster.SOUnitData.AttackRange, 30);
            if (FollowingPath) {
                if (Way != null) {
                    Gizmos.color = Color.green;
                    for (int i = 1; i < Way.Length; i++) {
                        Gizmos.DrawLine(Way[i - 1].WorldPosition, Way[i].WorldPosition);
                    }
                }
            }
        }
    }
}
