using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveable {
    public SaveData Save();
    public void Load(SaveData loadData);
}