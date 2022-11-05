using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Text textBgmVolume;
    public Text textSfxVolume;
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
            bgmVolumeSlider.value = -20;
            sfxVolumeSlider.value = -20;
        }

        this.bgmVolumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioControl();
        });
        this.sfxVolumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioControl();
        });

        AudioControl();
    }

    public void AudioControl()
    {
        float bgmVolume = bgmVolumeSlider.value;
        float sfxVolume = sfxVolumeSlider.value;

        if (bgmVolume == -40f)
            bgmVolume = -80;
        if (sfxVolume == -40f)
            sfxVolume = -80;

        audioMixer.SetFloat("BGM", bgmVolume);
        audioMixer.SetFloat("SFX", sfxVolume);
        //textBgmVolume.text = ((int)bgmVolume).ToString();
        //textSfxVolume.text = ((int)sfxVolume).ToString();

        PlayerPrefs.SetFloat("BgmVolume", bgmVolume);
        PlayerPrefs.SetFloat("SfxVolume", sfxVolume);
    }
}
