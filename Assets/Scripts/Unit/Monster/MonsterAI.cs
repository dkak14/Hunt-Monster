using System.Collections;
using System.Collections.Generic;
using BT;
using UnityEngine;
using System;
public abstract class MonsterAI : MonoBehaviour
{
    BTSequence root = new BTSequence();
    protected Monster monster;

    protected virtual void Awake() {
        Init();
        StartCoroutine(BehaviourProcess());
    }
    private void Init() {
        monster = GetComponent<Monster>();
        InitAI(root);
    }
    // AI √ ±‚»≠
    protected abstract void InitAI(BTSequence root);
    IEnumerator BehaviourProcess() {
        while (true) {
            root.Invoke();
            yield return null;
        }
    }
    private void OnDrawGizmos() {
        DrawGizmos(root);
    }
    private void DrawGizmos(BTCompositeNode compositeNode) {
        compositeNode.DrawGizmos();
        List<BTNode> nodes = compositeNode.GetChildren();
        for (int i = 0; i < nodes.Count; i++) {
            Gizmos.color = Color.white;
            if (nodes[i] is BTSelector) {
                DrawGizmos(nodes[i] as BTSelector);
            }
            else if (nodes[i] is BTSequence) {
                DrawGizmos(nodes[i] as BTSequence);
            }
            else {
                nodes[i].DrawGizmos();
            }
        }
    }
}
