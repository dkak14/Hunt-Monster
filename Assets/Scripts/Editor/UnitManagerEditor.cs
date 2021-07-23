using System.Collections.Generic;
using UnityEditor;
[CustomEditor(typeof(UnitManager),true), CanEditMultipleObjects]
public class UnitManagerEditor : Editor
{
    UnitManager unitManager;
    private void OnEnable() {
        unitManager = (UnitManager)target;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("��ȯ �� ����");
        List<string> nameList = unitManager.GetSpawnedUnitNameList();
        for(int i = 0; i < nameList.Count; i++) {
            List<Unit> SpawnedUnitList = unitManager.GetSpawnedUnitList(nameList[i]);
            int UnitNum = SpawnedUnitList.Count;
            EditorGUILayout.LabelField(nameList[i] + " : " + UnitNum);
        }
    }
}
