using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider bgmVolumeSlider;
    public Slider sfxVolumeSlider;

    public void Setting()
    {
        //float backGroundVolume = DataManager.instance.systemData.audioBackGroundVolume;
        //float masterVolume = DataManager.instance.systemData.audioMasterVolume;

        //bgmVolumeSlider.value = backGroundVolume;
        //sfxVolumeSlider.value = masterVolume;

    }

    public void Init()
    {
        this.bgmVolumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioControl();
        });
        this.sfxVolumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioControl();
        });


    }

    public void AudioControl()
    {
        float bgmVolume = bgmVolumeSlider.value;
        float sfxrVolume = sfxVolumeSlider.value;

        if (bgmVolume == -40f)
            bgmVolume = -80;
        if (sfxrVolume == -40f)
            sfxrVolume = -80;

        audioMixer.SetFloat("BGM", bgmVolume);
        audioMixer.SetFloat("SFX", sfxrVolume);

    }
}
