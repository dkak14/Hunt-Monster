using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using System.Threading;
using DG.Tweening;
public class PathfindingManager : SingletonBehaviour<PathfindingManager>
{
    static Queue<PathRequest> results = new Queue<PathRequest>();
    bool SearchEnd = true;
    protected override void Awake() {
        base.Awake();
        DOTween.SetTweensCapacity(500, 50);
    }
    private void Update() {
        if (results.Count > 0) {
            int resultCount = results.Count;

                for (int i = 0; i < resultCount; i++) {
                    //Debug.Log(resultCount);
                if (SearchEnd) {
                    SearchEnd = false;
                    PathRequest PathResult = results.Dequeue();
                    Thread thread = new Thread(() => {
                        PathResult.path = FindPath(PathResult.pathStart, PathResult.pathEnd);
                        PathResult.callback(PathResult.path);
                        SearchEnd = true;
                    });
                    thread.Start();
                }
                else
                    return;
                }
            
        }
    }
    public void PathFind(Vector3 finder, Vector3 target, Action<Node[]> callback) {
        PathRequest result = new PathRequest(finder, target, callback);
        PathFind(result);
    }
    void PathFind(PathRequest result) {
        lock (result) {
            results.Enqueue(result);
        }
    }
    public Node[] FindPath(Vector3 finder,Vector3 target) {

        Node StartNode = MapManager.Instance.GetNodeFromWorldposition(finder);
        Node EndNode = MapManager.Instance.GetNodeFromWorldposition(target);
        if (StartNode.IsWalkable && StartNode != null && EndNode != null && EndNode.IsWalkable) {
            List<Node> Way = new List<Node>();
            Heap<Node> OpenList = new Heap<Node>(MapManager.Instance.MapXSize * MapManager.Instance.MapYSize);
            HashSet<Node> CloseList = new HashSet<Node>();
            StartNode.parent = null;
            OpenList.Add(StartNode);
            int asd = 0;
            while (OpenList.Count > 0) {
                asd++;
                if (asd > 100000) {
                    Debug.Log("�ʰ�");
                    return null;
                }
                Node node = OpenList.RemoveFirst();
                CloseList.Add(node);
                if (node.WorldPosition == EndNode.WorldPosition) {
                    Debug.Log("ã��");
                    int asaa = 0;
                    while (node.parent != null) {
                        asaa++;
                        if (asaa > 10000) {
                            Debug.Log("�θ� �ʰ�");
                            return null;
                        }
                        Way.Add(node);
                        node = node.parent;
                    }
                    Way.Add(StartNode);
                    Node[] ways = Way.ToArray();
                    Array.Reverse(ways);
                    return ways;
                }

                Node[] neighborNodes = MapManager.Instance.GetNeighborTiles(node.X, node.Y, 1);
                // Ž�� ����� �ֺ� ��� Ž��
                for (int i = 0; i < neighborNodes.Length; i++) {
                    Node neighborNode = neighborNodes[i];
                    if (!neighborNode.IsWalkable || CloseList.Contains(neighborNode))
                        continue;
                    int nextGCost = node.G + GetDistance(node, neighborNode);
                    if (nextGCost < neighborNode.G || !OpenList.Contains(neighborNode)) {
                        neighborNode.H = GetDistance(neighborNode, EndNode);
                        neighborNode.G = nextGCost + neighborNode.Penalty;
                        neighborNode.parent = node;
                        if (!OpenList.Contains(neighborNode)) {
                            OpenList.Add(neighborNode);
                        }
                        else {
                            OpenList.UpdateItem(neighborNode);
                        }
                    }
                }
            }
        }
        return null;
    }

    public static int GetDistance(Node tile1, Node tile2) {
        float x = Mathf.Abs(tile1.X - tile2.X);
        float y = Mathf.Abs(tile1.Y - tile2.Y);
        if (x > y)
            return (int)(14 * y + 10 * (x - y));
        else
            return (int)(14 * x + 10 * (y - x));
    }
}
public class Node : IHeapItem<Node>{
    bool isWalkable;
    public bool IsWalkable => isWalkable;
    public int F { get { return G + H; } }
    public int G;
    public int H;
    int x;
    public int X => x;
    int y;
    public int Y => y;

    int heapIndex;
    public int HeapIndex { get => heapIndex; set { heapIndex = value; } }
    public int Penalty;

    public Vector3 WorldPosition;
    public Vector2 WorldVec2Position { get { return new Vector3(WorldPosition.x, WorldPosition.z); } }
    public Node parent;
    public Node(int x, int y,Vector3 worldPosition,int penalty, bool isWalkable) {
        this.isWalkable = isWalkable;
        this.x = x;
        this.y = y;
        Penalty = penalty;
        WorldPosition = worldPosition;
    }
    // �信 ������ 1 �ƴϸ� -1
    public int CompareTo(Node other) {
        //Debug.Log("�� " +WorldPosition + "  "+ F + "  " +other.WorldPosition+"  " +other.F);
        int compare = F.CompareTo(other.F);
        if (compare == 0) {
            compare = H.CompareTo(other.H);
        }
        return -compare;
    }
}