using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct WaveData {
    public int MonsterNum;
}
public class WaveManager : MonoBehaviour
{
    [SerializeField] List<Zone> SpawnZoneList = new List<Zone>();
    [SerializeField] List<WaveData> WaveDataList = new List<WaveData>();

    [SerializeField] float NextSpawnCul = 4;
    [SerializeField] float NextWaveCul = 5;

    [SerializeField] int LastSpawnedMonsterNum = 0; // 필드에 소환된 몬스터 수
    [SerializeField] int LastWaveMonsterNum; // 남은 소환 할 웨이브 몬스터
    List<Unit> UnitList = new List<Unit>();
    int CurrentWave = 0;

    bool isSpawnEnd = false;
    private void Awake() {
        UnitList = UnitManager.Instance.GetSpawnableUnitList();
        EventManager<MonsterEvent>.Instance.AddListener(MonsterEvent.Spawn, this, SpawnSensing);
        EventManager<MonsterEvent>.Instance.AddListener(MonsterEvent.Die, this, DieSensing);
    }
    private void Start() {
        StartCoroutine(C_WaveStart(CurrentWave));
    }
    void SpawnSensing(MonsterEvent eventType, Component sender, object param) {
        LastSpawnedMonsterNum++;
    }
    void DieSensing(MonsterEvent eventType, Component sender, object param) {
        LastSpawnedMonsterNum--;
        if (isSpawnEnd == true && LastSpawnedMonsterNum <= 0) {
            if (CurrentWave < WaveDataList.Count)
                StartCoroutine(C_WaitNextWave());
        }
    }

    //void CreateMonsterPool() {
    //    for(int i =0; i < UnitList.Count; i++) {
    //        Instantiate(UnitList)
    //    }
    //}
    IEnumerator C_WaveStart(int wave) {
        isSpawnEnd = false;
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.WaveStart, this, wave + 1);
        LastWaveMonsterNum = WaveDataList[wave].MonsterNum;
        Debug.Log(wave + 1 + "웨이브 시작");
        while(LastWaveMonsterNum > 0) {
            int SpawnMonsetNum;
            if(LastWaveMonsterNum > 10) {
                SpawnMonsetNum = 10;
                LastWaveMonsterNum -= 10;
            }
            else {
                SpawnMonsetNum = LastWaveMonsterNum;
                LastWaveMonsterNum = 0;
            }

            for(int i = 0; i < SpawnMonsetNum; i++) {
                SpawnMonster();
            }
            yield return new WaitForSeconds(NextSpawnCul);
        }
        isSpawnEnd = true;
        Debug.Log(wave + 1 + "웨이브 몬스터 소환 종료");
    }
    IEnumerator C_WaitNextWave() {
        Debug.Log(CurrentWave + 1 + "웨이브 종료");
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.WaveEnd, this, null);
        yield return new WaitForSeconds(NextWaveCul);
        CurrentWave++;
        StartCoroutine(C_WaveStart(CurrentWave));
    }
    void SpawnMonster() {
        Unit randomUnit = UnitList[Random.Range(0, UnitList.Count)];
        int spawnZoneIndex = Random.Range(0, SpawnZoneList.Count);

        Zone spawnZone = SpawnZoneList[spawnZoneIndex];
        Vector3 spawnPoint = spawnZone.GetRandomPointInZone();

        UnitManager.Instance.SpawnUnit(randomUnit.SOUnitData.UnitName, spawnPoint);
        //Instantiate(randomUnit, spawnPoint, Quaternion.identity);
    }

}
