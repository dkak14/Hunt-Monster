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
            EditorGUILayout.LabelField("�ִ� ü�� : " + MonsterData.MaxHP);
            EditorGUILayout.LabelField("���� ü�� : " + monster.HP);
            EditorGUILayout.LabelField("�̵� �ӵ� : " + MonsterData.Speed);
            EditorGUILayout.LabelField("���� �ӵ� : " + MonsterData.AttackRange);
            EditorGUILayout.LabelField("ȸ�� �ӵ� : " + MonsterData.RotateSpeed);
            EditorGUILayout.LabelField("�ּ� ��� ��� : " + MonsterData.MinDropGold);
            EditorGUILayout.LabelField("�ִ� ��� ��� : " + MonsterData.MaxDropGold);
        }
    }
}
