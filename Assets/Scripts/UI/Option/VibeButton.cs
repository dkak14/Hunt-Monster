using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
[RequireComponent(typeof(Button))]
public class VibeButton : MonoBehaviour
{
    [Inject] SoundSetting soundSetting;
    public GameObject On;
    public GameObject Off;
    Button button;
    bool isOn;
    void Start()
    {
        TryGetComponent(out button);
        button.onClick.AddListener(() => { Click(); Setting(); });
        Setting();
    }
    void Click() {
        if (isOn) {
            isOn = false;
        }
        else {
            isOn = true;
        }
    }
    void Setting() {
        if (isOn) {
            On.gameObject.SetActive(true);
            Off.gameObject.SetActive(false);
        }
        else {
            On.gameObject.SetActive(false);
            Off.gameObject.SetActive(true);
        }
    }
}
