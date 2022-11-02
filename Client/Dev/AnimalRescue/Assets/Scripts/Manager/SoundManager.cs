using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioClip[] audiobgmArr;
    private AudioSource audioSource;
    private int bgmCount;
    private int currentIndex;

    private Coroutine playSoundRoutine;

    private void Awake()
    {
        SoundManager.instance = this;
    }

    public void Init()
    {
        audioSource = GetComponent<AudioSource>();
        bgmCount = audiobgmArr.Length;
        currentIndex = 0;
        //StartCoroutine(this.PlaySoundRoutine());
        //PlaySound();
    }
    public void PlayBGMSound()
    {
        //audioSource.clip = audiobgmArr[currentIndex];
        //audioSource.Play();
        if (playSoundRoutine != null)
            StopBGMSound();
        playSoundRoutine = StartCoroutine(this.PlayBGMSoundRoutine());
    }

    public void StopBGMSound()
    {
        StopCoroutine(playSoundRoutine);
        audioSource.Stop();
        playSoundRoutine = null;
    }

    private IEnumerator PlayBGMSoundRoutine()
    {
        while (true)
        {
            if (currentIndex == bgmCount)
            {
                currentIndex = 0;
            }
            yield return new WaitForSeconds(0.5f);
            if (!audioSource.isPlaying)
            {
                audioSource.clip = audiobgmArr[currentIndex];
                audioSource.Play();
                currentIndex++;
            }
        }
    }
}
