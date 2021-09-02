using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Zenject;
[RequireComponent(typeof(Slider))]
public class AudioSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] SoundType Type;
    [Inject] SoundSetting soundSetting;
    private void Start() {
        TryGetComponent(out slider);
        slider.minValue = -40;
        slider.maxValue = 0;

        float volume = soundSetting.GetAudioVolume(Type);
        slider.value = volume;
        Debug.Log(slider == null);
        slider.onValueChanged.AddListener((value) => {soundSetting.SetAudioVolume(Type, value);});
    }
}
