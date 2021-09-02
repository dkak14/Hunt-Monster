using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject optionImage;
    public bool isVibration;
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) {
            if (Input.GetKey(KeyCode.Escape)) {
                optionImage.SetActive(true);
                Debug.Log("뒤로가기");
            }
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            optionImage.SetActive(true);
            Debug.Log("뒤로가기");
        }
    }
}
