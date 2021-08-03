using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    //오디오 볼륨 조정 (오디오믹서 사용)//

    public AudioMixer audioMixer;
    public Slider[] audioSlider;

    public void AudioControl(int sliderID)
    {
        float volume = audioSlider[sliderID].value;
        string parameter = SliderID(sliderID); 

        if (volume == -40f) audioMixer.SetFloat(parameter, -80);
        else audioMixer.SetFloat(parameter, volume);
    }
    public void AudioOnOff(int sliderID)
    {
        float volume = audioSlider[sliderID].value;
        string parameter = SliderID(sliderID);

        volume = volume <= -40f ? 0f : -80f;
        audioMixer.SetFloat(parameter, volume);

        audioSlider[sliderID].value = volume <= -80f ? -40f : 0f;
    } //임시..
    private string SliderID(int ID)
    {
        switch (ID)
        {
            case 0:
                return "Master";
            case 1:
                return "BGM";
            case 2:
                return "SFX";
        }

        return null;
    }

}