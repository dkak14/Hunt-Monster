using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPanel : MonoBehaviour
{
    [SerializeField] GameObject Panel;
    void Update()
    {
        
    }

    public void Close() {
        Time.timeScale = 1;
        Panel.SetActive(false);
    }
    public void GoMain() {

    }
}
