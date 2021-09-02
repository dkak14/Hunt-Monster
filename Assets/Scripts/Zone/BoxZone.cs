using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxZone : Zone
{
    public override void AddPoint(int x, int z) {
        if (Points.Count >= 4)
            return;

        base.AddPoint(x, z);
    }

    public override void MovePoint(int index, Vector3 point) {

        int previousIndex = GetItem(index - 1);
        int nextIndex = GetItem(index + 1);

        if (Points[nextIndex].z == Points[index].z) {
            base.MovePoint(index, point);
            base.MovePoint(nextIndex, new Vector3(Points[nextIndex].x, Points[nextIndex].y, Points[index].z));
            base.MovePoint(previousIndex, new Vector3(Points[index].x, Points[previousIndex].y, Points[previousIndex].z));
        }

        else if (Points[nextIndex].x == Points[index].x) {
            base.MovePoint(index, point);
            base.MovePoint(nextIndex, new Vector3(Points[index].x, Points[nextIndex].y, Points[nextIndex].z));
            base.MovePoint(previousIndex, new Vector3(Points[previousIndex].x, Points[previousIndex].y, Points[index].z));
        }
    }

    int GetItem(int index) {
        if (index > 3)
            return 0;
        else if (index < 0)
            return 3;
        else
            return index;
    }
    public Vector2 GetSize() {
        int x = (int)Mathf.Abs(Points[0].x - Points[2].x);
        int y = (int)Mathf.Abs(Points[0].z - Points[2].z);
        return new Vector2(x, y);
    }
    public Vector2 GetCenterVec2Pos() {
        float centerX = (Points[2].x - Points[0].x) / 2;
        float centerY = (Points[2].z - Points[0].z) / 2;
        return new Vector2(Points[0].x + centerX, Points[0].z + centerY);
    }
    public Vector2 GetCenterVec3Pos() {
        float centerX = (Points[2].x - Points[0].x) / 2;
        float centerY = (Points[2].z - Points[0].z) / 2;
        return new Vector3(Points[0].x + centerX,0, Points[0].z + centerY);
    }
}
