using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Monster),true),CanEditMultipleObjects]
public class MonsterEditor : Editor
{
    Monster monster;
    SOMonster MonsterData;
    private void OnEnable() {
        monster = (Monster)target;
    }
    public override void OnInspectorGUI() {
        base.OnInspectorGUI();
        if (monster.SOUnitData != null) {
            if (MonsterData == null)
                MonsterData = (SOMonster)monster.SOUnitData;
            EditorGUILayout.LabelField("최대 체력 : " + MonsterData.MaxHP);
            EditorGUILayout.LabelField("현재 체력 : " + monster.HP);
            EditorGUILayout.LabelField("이동 속도 : " + MonsterData.Speed);
            EditorGUILayout.LabelField("공격 속도 : " + MonsterData.AttackRange);
            EditorGUILayout.LabelField("회전 속도 : " + MonsterData.RotateSpeed);
            EditorGUILayout.LabelField("최소 드롭 골드 : " + MonsterData.MinDropGold);
            EditorGUILayout.LabelField("최대 드롭 골드 : " + MonsterData.MaxDropGold);
        }
    }
}
