using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : SingletonBehaviour<MapManager>
{
    [SerializeField] BoxZone mapZone;
    public BoxZone MapZone => mapZone;
    [SerializeField] BoxZone renderMapZone;
    public BoxZone RenderMapZone => renderMapZone;
    public int MapXSize;
    public int MapYSize;
    Vector3 MapCenter;
    [Range(0.2f, 1)]
    public float NodeSize;
    public Map m_map;
    public Map m_Map => m_map;

    [SerializeField, Range(0, 100)]
    int XDivision;
    [SerializeField, Range(0, 100)]
    int YDivision;


    int MapNodeXCount;
    int MapNodeYCount;
    

    [HideInInspector]
    public List<MapRegion> Regions;
    List<MapRegion> SearchRegions = new List<MapRegion>();
    public MapRegion[,] RegionNodes;
    Vector2 RegionSize;
    Vector3 RegionsCorner;


    [SerializeField] bool PaintGrid;

    Transform Player;

    float RenderDistance;
    float WorldScreenWidth;
    protected override void Awake() {
        base.Awake();
        Vector2 MapSize = MapZone.GetSize();
        Vector2 MapVec2Center = MapZone.GetCenterVec2Pos();

        MapXSize = (int)MapSize.x;
        MapYSize = (int)MapSize.y;

        MapCenter = new Vector3(MapVec2Center.x, 0, MapVec2Center.y);
        MapNodeXCount = Mathf.RoundToInt(MapXSize / NodeSize);
        MapNodeYCount = Mathf.RoundToInt(MapYSize / NodeSize);
        m_map = new MapGenerator().MapGenerate(MapCenter, MapXSize, MapYSize, NodeSize);
        GenerateRegions();
        for(int i = 0; i < Regions.Count; i++) {
            Regions[i].EnabledObjectsRender(false);
        }
        CreateCollider();
        Camera mainCamera = Camera.main;
        Vector3 screenCorner1 = mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenCorner2 = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        WorldScreenWidth = screenCorner2.x - screenCorner1.x;
        RenderDistance = WorldScreenWidth + Mathf.Max(RegionSize.x * 0.5f, RegionSize.y * 0.5f);
    }
    private void Start() {
        Player = UnitManager.Instance.GetSpawnedUnitList("Player")[0].transform;
    }
    public bool Test;
    private void Update() {
        if(Test)
        UpdateVisibleRegion();
        else
        UpdateVisibleRegion2();
    }
    void UpdateVisibleRegion() {
        RenderDistance = WorldScreenWidth + Mathf.Max(RegionSize.x * 0.5f, RegionSize.y * 0.5f);
        int RegionRangeX = (int)(WorldScreenWidth % RegionSize.x + 1);
        //Debug.Log(RenderDistance + " " + RegionRangeX +" "+ WorldScreenWidth);
        //Debug.Log(GetRegionFromWorldPosition(Player.position));
        Queue<MapRegion> regionQueue = new Queue<MapRegion>();
        Vector2 playerRegion = GetRegionFromWorldPosition(Player.position);
        for (int x = -RegionRangeX; x < RegionRangeX; x++) {
            for (int y = -RegionRangeX; y < RegionRangeX; y++) {
                int SearchX = (int)playerRegion.x + x;
                int SearchY = (int)playerRegion.y + y;
                if (isInRegionNodes(SearchX, SearchY)) {
                    MapRegion region = RegionNodes[SearchX, SearchY];
                    regionQueue.Enqueue(region);
                }
            }
        }
        while(regionQueue.Count > 0) {
            MapRegion region = regionQueue.Dequeue();

            float Dst = Vector3.Distance(Player.position, region.Center);
            if (Dst < RenderDistance) {
                if (!region.isActive)
                    region.EnabledObjectsRender(true);
            }
            else if (Dst > RenderDistance) {
                if (region.isActive)
                    region.EnabledObjectsRender(false);
            }
        }
    }
    void UpdateVisibleRegion2() {
        RenderDistance = WorldScreenWidth + Mathf.Max(RegionSize.x * 0.5f, RegionSize.y * 0.5f);
        int RegionRangeX = (int)(WorldScreenWidth % RegionSize.x + 1);
        //Debug.Log(RenderDistance + " " + RegionRangeX + " " + WorldScreenWidth);
        //Debug.Log(GetRegionFromWorldPosition(Player.position));
        for(int i = 0; i < Regions.Count; i++) {
            MapRegion region = Regions[i];
            float Dst = Vector3.Distance(Player.position, region.Center);
            if (Dst < RenderDistance) {
                if (!region.isActive)
                    region.EnabledObjectsRender(true);
            }
            else if (Dst > RenderDistance) {
                if (region.isActive)
                    region.EnabledObjectsRender(false);
            }
        }
    }
    Vector2 GetRegionFromWorldPosition(Vector3 worldPos) {
        float percentX = ((worldPos.x - RegionsCorner.x) / RegionSize.x / RegionNodes.GetLength(0));
        float percentY = ((worldPos.z - RegionsCorner.z) / RegionSize.y / RegionNodes.GetLength(1));
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);


        int x = Mathf.RoundToInt(RegionNodes.GetLength(0) * percentX);
        int y = Mathf.RoundToInt(RegionNodes.GetLength(1) * percentY);
        MapRegion region = RegionNodes[x, y];
        return new Vector2(x, y);
    }
    void GenerateRegions() {
        int regionsCount = (XDivision + 1) * (YDivision + 1);
        RegionNodes = new MapRegion[XDivision + 1, YDivision + 1];
        Regions = new List<MapRegion>(regionsCount);

        float increaseX = MapXSize / (float)(XDivision);
        float increaseY = MapYSize / (float)(YDivision);
        RegionSize = new Vector2(increaseX, increaseY);
        float StartX = m_Map.MapCorner.x + increaseX * 0.5f;
        float StartY = m_Map.MapCorner.z + increaseY * 0.5f;

        Vector3 regionCenter;
        Vector3 halfExtent = new Vector3(increaseX * 0.5f, 5, increaseY * 0.5f);
        RegionsCorner = new Vector3(StartX - halfExtent.x, 0,StartY - halfExtent.z);
        for (int x = 0; x < XDivision + 1; x++) {
            for (int y = 0; y < YDivision + 1; y++) {
                regionCenter = new Vector3(StartX + increaseX * x, 0, StartY + increaseY * y);
                RaycastHit[] hits = Physics.BoxCastAll(regionCenter, halfExtent, Vector3.up, Quaternion.identity, 100, 1 << LayerMask.NameToLayer("Object"));

                List<MapObject> mapObjects = new List<MapObject>();
                int objectNum = 0;
                for (int i = 0; i < hits.Length; i++) {
                    Transform hitTransform = hits[i].transform;
                    if (hitTransform.GetComponent<MapObject>()) {
                        MapObject mapObject;
                        hitTransform.TryGetComponent(out mapObject);
                        mapObjects.Add(mapObject);
                        objectNum++;
                    }
                }
                MapRegion mapRegion = new MapRegion(mapObjects, regionCenter, new Vector2(increaseX, increaseY));
                RegionNodes[x, y] = mapRegion;
                Regions.Add(mapRegion);
            }
        }
    }
    bool isInRegionNodes(int x, int y) {
        if(0 <= x && x < RegionNodes.GetLength(0) && 0 <= y && y < RegionNodes.GetLength(1)) {
            return true;
        }
        return false;
    }
    void CreateCollider() {
        BoxCollider collider = transform.gameObject.AddComponent<BoxCollider>();
        collider.size = new Vector3(MapXSize, 1, MapYSize);
        collider.center = new Vector3(MapCenter.x, -0.5f, MapCenter.y);
    }
    private void OnDrawGizmos() {
        if (PaintGrid)
            if (m_map != null ) {
            Vector3 vec = new Vector3(NodeSize, NodeSize, NodeSize);
            for (int y = 0; y < MapNodeYCount; y++) {
                for (int x = 0; x < MapNodeXCount; x++) {
                    if (m_map.MapData[x, y].IsWalkable)
                        Gizmos.color = Color.white;
                    else
                        Gizmos.color = Color.red;
                    Gizmos.DrawCube(m_map.MapData[x, y].WorldPosition, vec);
                }
            }
        }
    }
}
public class MapRegion {
    List<MapObject> MapObjectList;
    Vector3 center;
    public bool isActive;
    public Vector3 Center => center;
    Vector2 size;
    public Vector2 Size => size;
    public MapRegion(List<MapObject> MapObjectList, Vector3 Center, Vector2 Size) {
        this.MapObjectList = MapObjectList;
        this.center = Center;
        this.size = Size;
    }
    
    public void EnabledObjectsRender(bool active) {
        isActive = active;
        for (int i = 0; i < MapObjectList.Count; i++) {
            MapObjectList[i].SetActiveRenderer(active);
        }
    }
}