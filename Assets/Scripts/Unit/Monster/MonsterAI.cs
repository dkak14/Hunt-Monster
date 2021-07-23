using System.Collections;
using System.Collections.Generic;
using BT;
using UnityEngine;
using System;
public abstract class MonsterAI : MonoBehaviour
{
    BTSequence root = new BTSequence();
    protected Monster monster;

    bool isProcessing;
    protected virtual void Awake() {
        Init();
    }
    protected virtual void Start() {
        StartProcess();
    }
    private void Init() {
        monster = GetComponent<Monster>();
        InitAI(root);
    }
    // AI √ ±‚»≠
    protected abstract void InitAI(BTSequence root);
    public void StartProcess() {
        StopAllCoroutines();
        StartCoroutine(BehaviourProcess());
        isProcessing = true;
    }
    public void StopProcess() {
        StopAllCoroutines();
        isProcessing = false;
    }
    IEnumerator BehaviourProcess() {
        while (true) {
            root.Invoke();
            yield return null;
        }
    }
    private void OnDrawGizmos() {
        if(isProcessing)
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
