using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UnitManager : SingletonBehaviour<UnitManager>
{
    [SerializeField] List<Unit> UnitList = new List<Unit>(); // ���������� ���� ����Ʈ
    Dictionary<string, Unit> UnitDataDic = new Dictionary<string, Unit>();

    List<string> SpawnedUnitNameList = new List<string>();
    Dictionary<string, List<Unit>> SpawnedUnitDic = new Dictionary<string, List<Unit>>(); // �ʵ忡 ���� �� ���ֵ�
    private void Awake() {
        for(int i = 0; i < UnitList.Count; i++) {
            UnitDataDic.Add(UnitList[i].SOUnitData.UnitName, UnitList[i]);
        }
        EventManager<UnitEvent>.Instance.AddListener(UnitEvent.Spawn, this, SpawnUnitEvent);
        EventManager<UnitEvent>.Instance.AddListener(UnitEvent.Die, this, DieUnitEvent);
    }
    // ���� ���������� ���ָ� ���� �ϴ� �Լ�
    public void SpawnUnit(string UnitName, Vector3 SpawnPos) {
        if (!SpawnedUnitDic.ContainsKey(UnitName))
            SpawnedUnitDic.Add(UnitName, new List<Unit>());

        Instantiate(UnitDataDic[UnitName], SpawnPos, Quaternion.identity);
    }
    public List<Unit> GetSpawnedUnitList(string UnitName) {
        if (SpawnedUnitDic.ContainsKey(UnitName)) {
            return SpawnedUnitDic[UnitName];
        }
        else
            return null;
    }
    // ���� ���� �����ϰ� �����Ǹ� SpawnedUnitDic�� �߰����� �����Ѵ�
    public void SpawnUnitEvent(UnitEvent eventType, Component Sender, object param) {
        if (Sender is Unit) {
            Unit unit = (Unit)Sender;
            if (!SpawnedUnitDic.ContainsKey(unit.SOUnitData.UnitName)) {
                SpawnedUnitDic.Add(unit.SOUnitData.UnitName, new List<Unit>());
                SpawnedUnitNameList.Add(unit.SOUnitData.UnitName);
            }

            SpawnedUnitDic[unit.SOUnitData.UnitName].Add(unit);
            Debug.Log("���� ���� ���� " + unit.SOUnitData.UnitName);
        }
    }
    // ���� ���� �����ϰ� ������ SpawnedUnitDic�� ����
    public void DieUnitEvent(UnitEvent eventType, Component Sender, object param) {
        if (Sender is Unit) {
            Unit unit = (Unit)Sender;
            SpawnedUnitDic[unit.SOUnitData.UnitName].Remove(unit);

            if(SpawnedUnitDic[unit.SOUnitData.UnitName].Count == 0) {
                SpawnedUnitNameList.Remove(unit.SOUnitData.UnitName);
            }
        }
    }

    public List<string> GetSpawnedUnitNameList() {
        return SpawnedUnitNameList;
    }
}
