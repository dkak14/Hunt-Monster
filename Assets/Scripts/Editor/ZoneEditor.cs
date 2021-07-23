using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Zone), true),CanEditMultipleObjects]
public class ZoneEditor : Editor
{
    Zone zone;
    MeshFilter meshFilter;
    MeshRenderer meshRenderer;
    Material ZoneMaterial;
    List<Vector2> Points;
    float Height;
    Vector3 Size = new Vector3(3, 3, 3);

    MaterialPropertyBlock MPB;
    protected virtual void OnEnable() {
        zone = (Zone)target;
        zone.transform.gameObject.layer = LayerMask.NameToLayer("UI");
        Height = serializedObject.FindProperty("Height").floatValue;
        meshFilter = zone.GetComponent<MeshFilter>();
        meshRenderer = zone.GetComponent<MeshRenderer>();
        ZoneMaterial = LoadMaterial();
        MPB = new MaterialPropertyBlock();
        if (meshFilter.sharedMesh == null) {
            Mesh mesh = new Mesh();
            mesh.name = "ZoneMesh";
            meshFilter.sharedMesh = mesh;
        }
        if(meshRenderer.sharedMaterial == null) {
            meshRenderer.sharedMaterial = ZoneMaterial;
        }
    }

    protected virtual void OnSceneGUI() {
        Event e = Event.current;
        zone.transform.position = Vector3.zero;
        if (e.type == EventType.MouseDown && e.button == 1 && e.shift) {
            Vector3 pos = Camera.current.ScreenToWorldPoint(e.mousePosition);
            zone.AddPoint((int)pos.x, (int)pos.y);
            Debug.Log("추가");
        }
        if (zone.Points.Count > 0) {
            // 선 잇기
            for (int i = 1; i < zone.Points.Count; i++) {
                Handles.DrawLine(zone.Points[i - 1], zone.Points[i]);
            }
            Handles.DrawLine(zone.Points[0], zone.Points[zone.Points.Count - 1]);
            // 핸들 달기
            for (int i = 0; i < zone.Points.Count; i++) {
                Vector3 vec = Handles.FreeMoveHandle(zone.Points[i], Quaternion.identity, 0.5f, Vector2.zero, Handles.DotHandleCap);
                Handles.BeginGUI();
                if (GUI.Button(new Rect(HandleUtility.WorldToGUIPoint(zone.Points[i]) + Vector2.right * 10, new Vector2(40,30)), i.ToString())) {
                    RemovePoint(i);
                }
                Handles.EndGUI();
                zone.MovePoint(i, vec);
            }
            // 메쉬 그리기
            DrawMesh();
        }
    }
    protected virtual void RemovePoint(int Index) {
        zone.Points.RemoveAt(Index);
    }
    void DrawMesh() {
        if (zone.Points.Count > 2) {
            Mesh mesh = meshFilter.sharedMesh;
            int triangleCount = zone.Points.Count - 2;
            int[] triangles = new int[triangleCount * 3];
            List<Vector2> vec = new List<Vector2>();
            for(int i = 0; i < zone.Points.Count; i++) {
                vec.Add(new Vector2(zone.Points[i].x, zone.Points[i].z));
            }
            CompositeShape.triangulate(vec.ToArray(), out triangles);
            mesh.triangles = triangles;
            mesh.vertices = zone.Points.ToArray();
            zone.triangles = triangles;
            MPB.SetColor("_Color", zone.ZoneColor);
            meshRenderer.sharedMaterial = ZoneMaterial;
            meshRenderer.SetPropertyBlock(MPB);
            meshRenderer.sharedMaterial.color = zone.ZoneColor;
        }
    }
    Material LoadMaterial() {
        string MaterialPath = "Assets/Material/Zone.mat";
        Material material = (Material)AssetDatabase.LoadAssetAtPath(MaterialPath, typeof(Material));
        return material;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
    }
}
