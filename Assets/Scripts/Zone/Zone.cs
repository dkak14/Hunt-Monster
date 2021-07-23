using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Zone : MonoBehaviour
{
    [SerializeField] public Color ZoneColor = new Color(1,1,1,0.5f);
    [SerializeField] float Height;
    [HideInInspector] public List<Vector3> Points = new List<Vector3>();
    [HideInInspector] public int[] triangles;

    public virtual void AddPoint(int x, int z) {
        Points.Add(new Vector3(x,0,z));
    }
    public virtual void MovePoint(int index, Vector3 point) {
        Points[index]= new Vector3((int)point.x, 0 , (int)point.z);
    }
    public Vector3 GetRandomPointInZone() {
        if (Points.Count > 0) {
            int Index = Random.Range(0, Points.Count - 2);
            float range1;
            float range2;
            int or = Random.Range(0, 2);
            if(or == 1) {
                range1 = Random.Range((float)0, 1);
                range2 = Random.Range((float)0, 1 - range1);
            }
            else {
                range2 = Random.Range((float)0, 1);
                range1 = Random.Range((float)0, 1 - range2);
            }
            float dir1Dst = Vector3.Distance(Points[triangles[Index * 3 + 1]], Points[triangles[Index * 3]]) * range1;
            float dir2Dst = Vector3.Distance(Points[triangles[Index * 3 + 2]], Points[triangles[Index * 3]]) * range2;
            Vector3 dir1 = (Points[triangles[Index * 3 + 1]] - Points[triangles[Index * 3]]).normalized;
            Vector3 dir2 = (Points[triangles[Index * 3 + 2]] - Points[triangles[Index * 3]]).normalized;
            Vector3 pos = Points[triangles[Index * 3]] + (dir1 * dir1Dst) + (dir2 * dir2Dst);
            return pos;
        }
        return Vector2.zero;
    }
}
