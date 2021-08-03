using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTest : MonoBehaviour
{
    SaveData data;
    void Start()
    {
        SaveData save = new SaveData("Test1", new SaveData("테스트용 텍스트"));
        SaveSystem.Save(1, "SaveTest", save);
        data = SaveSystem.Load(1, "SaveTest");
        Debug.Log(data.GetData("Test1").GetString());
    }
}

public class SaveDataGroup{
    string GroupName;
    public SaveDataGroup(string groupName) {
        GroupName = groupName;
    }
    public void AddData() {

    }
}
