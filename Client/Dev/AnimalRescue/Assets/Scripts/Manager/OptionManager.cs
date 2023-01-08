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

    private void OnApplicationPause(bool pause)
    {
        AudioSettings.Reset(AudioSettings.GetConfiguration());
        // 일시정지 했을 때 현재 슬라이드바 값을 저장하고 게임이 실행될 때 볼륨값 다시 세팅
        if (pause)
        {
            PlayerPrefs.Save();
        }
        else
        {
            float bgmVolume = PlayerPrefs.GetFloat("BgmVolume");
            float sfxVolume = PlayerPrefs.GetFloat("SfxVolume");
            AudioControl(bgmVolume, sfxVolume);
        }
    }

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
            AudioControl(bgmVolumeSlider.value, sfxVolumeSlider.value);
        });
        this.sfxVolumeSlider.onValueChanged.AddListener((value) =>
        {
            AudioControl(bgmVolumeSlider.value, sfxVolumeSlider.value);
        });
        AudioControl(bgmVolumeSlider.value, sfxVolumeSlider.value);
    }

    public void AudioControl(float bgmValue, float sfxValue)
    {
        float bgmVolume = bgmValue;
        float sfxVolume = sfxValue;

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
