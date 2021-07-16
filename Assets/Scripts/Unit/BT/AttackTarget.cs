using UnityEngine;
using BT;
namespace BT {
    public class AttackTarget : BTNode {
        Unit unit;
        float sensingRange;
        float attakRange;
        LayerMask layerMask;

        bool FIndTarget = false;
        /// <summary>
        /// �ν� ���� �ȿ� Ÿ���� ������ Ÿ���� �Ѿư�
        /// </summary>
        /// <param name="unit">Ÿ���� "ã��" ����</param>
        /// <param name="range">Ÿ�� �ν� ����</param>
        /// <param name="layerMask">Ÿ���� ���̾�</param>
        public AttackTarget(Unit unit, float sensingRange, float attakRange, LayerMask layerMask) {
            this.unit = unit;
            this.sensingRange = sensingRange;
            this.attakRange = attakRange;
            this.layerMask = layerMask;
        }
        public override bool Invoke() {
            RaycastHit[] hit = Physics.SphereCastAll
                (unit.transform.position, sensingRange, Vector3.up, sensingRange, layerMask);
            if (hit.Length > 0) {
                FIndTarget = true;

                Transform trans = hit[0].collider.transform;
                float dst = Vector2.Distance(unit.GetVec2Position(), new Vector2(trans.position.x, trans.position.z));

                if (dst >= unit.AttackRange) {
                    Vector3 dir3 = (trans.position - unit.transform.position).normalized;
                    Vector3 dir2 = new Vector3(dir3.x, 0, dir3.z);
                    unit.Move(dir2);
                    unit.Rotate(trans);
                }
                else
                    unit.Attack();
                return true;
            }
            else {
                FIndTarget = false;
                return false;
            }
        }
        public override void DrawGizmos() {
            Color gizmoColor = FIndTarget ? Color.green : Color.red;
            Gizmos.color = gizmoColor;
            MyGizmos.DrawWireCicle(unit.transform.position, sensingRange, 30);
        }
    }
}
public static class MyGizmos {
    public static void DrawWireCicle(Vector3 center,float radius, int smoothNum) {
        int angle = 360 / smoothNum;
        Vector3[] dir = new Vector3[smoothNum];
        dir[0] = center + new Vector3(Mathf.Cos(0), 0, Mathf.Sin(0)) * radius;
        for (int i = 1; i < smoothNum; i++) {
            float dirAngle = angle * i;
            dirAngle *= Mathf.Deg2Rad;
            Vector3 dirvec = new Vector3(Mathf.Cos(dirAngle), 0, Mathf.Sin(dirAngle)) * radius;
            dir[i] = center + dirvec;
            Gizmos.DrawLine(dir[i - 1], dir[i]);
        }
        Gizmos.DrawLine(dir[smoothNum - 1], dir[0]);
    }
}