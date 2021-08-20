using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBehaviour<MapManager>
{
    [SerializeField] BoxZone MapZone;
    public int MapXSize;
    public int MapYSize;
    Vector3 MapCenter;
    Vector3 MapCorner;
    [Range(0.2f, 1)]
    public float NodeSize;
    public Node[,] MapData;
    Node[] way;

    int MapNodeXCount;
    int MapNodeYCount;
    Vector3 size;
    List<Node> cantWalkableNode = new List<Node>();
    protected override void Awake() {
        base.Awake();
        Vector2 MapSize = MapZone.GetSize();
        Vector2 MapVec2Center = MapZone.GetCenterPos();

        MapXSize = (int)MapSize.x;
        MapYSize = (int)MapSize.y;

        MapCenter = new Vector3(MapVec2Center.x, 0, MapVec2Center.y);

        Vector3 LeftDownCorner = MapZone.Points[0];
        for(int i = 1; i < MapZone.Points.Count; i++) {
            if(MapZone.Points[i].x < LeftDownCorner.x && MapZone.Points[i].z < LeftDownCorner.z) {
                LeftDownCorner = MapZone.Points[i];
            }
        }
        MapCorner = LeftDownCorner;

        MapNodeXCount = Mathf.RoundToInt(MapXSize / NodeSize);
        MapNodeYCount = Mathf.RoundToInt(MapYSize / NodeSize);
        size = new Vector3(NodeSize, NodeSize, NodeSize);
        MapData = new Node[MapNodeXCount, MapNodeYCount];

        int layermask = 1 << LayerMask.NameToLayer("Object");
        for (int y = 0; y < MapNodeYCount; y++) {
            for (int x = 0; x < MapNodeXCount; x++) {
                float NodeX = x * NodeSize + MapCorner.x;
                float NodeY = y * NodeSize + MapCorner.z;
                Vector3 worldPosition = new Vector3(NodeX, 0, NodeY);
                RaycastHit ray;
                if (Physics.Raycast(new Vector3(NodeX, 10, NodeY), Vector3.down, out ray, 50, layermask)) {
                    MapData[x, y] = new Node(x, y, worldPosition, 0, false);
                    cantWalkableNode.Add(MapData[x, y]);
                }
                else {
                    MapData[x, y] = new Node(x, y, worldPosition, 0, true);
                }
            }
        }
        for (int i = 0; i < cantWalkableNode.Count; i++) {
            Node[] neighborNodes = GetNeighborTiles(cantWalkableNode[i].X, cantWalkableNode[i].Y, 2);
            for (int k = 0; k < neighborNodes.Length; k++) {
                neighborNodes[k].Penalty += 5;
            }
        }
        CreateCollider();
    }
    void CreateCollider() {
        BoxCollider collider = transform.gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(MapXSize, 1, MapYSize);
        collider.center = new Vector3(MapCenter.x, -0.5f, MapCenter.y);
    }
    public Node GetNodeFromWorldposition(Vector3 worldPosition) {
        float percentX = ((worldPosition.x - MapCorner.x) / NodeSize / MapNodeXCount);
        float percentY = ((worldPosition.z - MapCorner.z) / NodeSize / MapNodeYCount);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);


        int x = Mathf.RoundToInt(MapNodeXCount * percentX);
        int y = Mathf.RoundToInt(MapNodeYCount * percentY);
        if (isPositionInMapNodes(x, y)) {
            Node node = MapData[x,y];
            return node;
        }
        return null;
    }
    public bool isPositionInMapNodes(float x, float y) {
        if (0 <= x * NodeSize && x * NodeSize < MapNodeXCount && 0 <= y * NodeSize && y * NodeSize < MapNodeYCount)
            return true;
        return false;
    }
    public Node[] GetNeighborTiles(int tileX, int tileY, int range) {
        List<Node> tileList = new List<Node>();
        for(int x = tileX - range; x <= tileX+ range; x++) {
            for (int y = tileY - range; y <= tileY + range; y++) {
                if (x == tileX && y == tileY)
                    continue;

                if(0 <= x && x < MapNodeXCount && 0 <= y && y < MapNodeYCount) {
                    tileList.Add(MapData[x, y]);
                }
            }
        }
        return tileList.ToArray();
    }

}
