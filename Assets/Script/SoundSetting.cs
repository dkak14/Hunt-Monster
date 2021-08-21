using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundSetting : MonoBehaviour
{
    //오디오 볼륨 조정 (오디오믹서 사용)//
    enum soundID { Master, BGM, SFX };

    public AudioMixer audioMixer;
    public Slider[] audioSlider;
    public SoundSO soundSO;

    public void AudioControl(int sliderID)
    {
        float volume = audioSlider[sliderID].value;
        string parameter = SliderID(sliderID);

        if (volume == -40f) audioMixer.SetFloat(parameter, -80);
        else audioMixer.SetFloat(parameter, volume);

        if (sliderID == 0)
            soundSO.MasterVolume = volume;
        else if (sliderID == 1)
            soundSO.BGMVolume = volume;
        else if (sliderID == 2)
            soundSO.SFXVolume = volume;
    }
    public void AudioOnOff(int sliderID)
    {
        float volume = audioSlider[sliderID].value;
        string parameter = SliderID(sliderID);

        volume = volume <= -40f ? 0f : -80f;
        audioMixer.SetFloat(parameter, volume);

        audioSlider[sliderID].value = volume <= -80f ? -40f : 0f;

        if (sliderID == 0)
            soundSO.MasterVolume = volume;
        else if (sliderID == 1)
            soundSO.BGMVolume = volume;
        else if (sliderID == 2)
            soundSO.SFXVolume = volume;
    } //임시..

    private string SliderID(int ID)
    {
        switch (ID)
        {
            case (int)soundID.Master:
                return "Master";
            case (int)soundID.BGM:
                return "BGM";
            case (int)soundID.SFX:
                return "SFX";
        }

        return null;
    }

}