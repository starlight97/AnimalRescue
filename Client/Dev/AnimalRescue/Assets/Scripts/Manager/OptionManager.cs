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

    public void Init()
    {
        if (PlayerPrefs.HasKey("BgmVolume") && PlayerPrefs.HasKey("SfxVolume"))
        {
            bgmVolumeSlider.value = PlayerPrefs.GetFloat("BgmVolume");
            sfxVolumeSlider.value = PlayerPrefs.GetFloat("SfxVolume");
        }
        else
        {
            bgmVolumeSlider.value = 0.5f;
            sfxVolumeSlider.value = 0.5f;
        }

        this.bgmVolumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioControl();
        });
        this.sfxVolumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioControl();
        });

        audioMixer.SetFloat("BGM", bgmVolumeSlider.value);
        audioMixer.SetFloat("SFX", sfxVolumeSlider.value);
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

        PlayerPrefs.SetFloat("BgmVolume", bgmVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxrVolume);
    }
}
