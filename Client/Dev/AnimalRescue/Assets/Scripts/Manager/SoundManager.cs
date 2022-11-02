using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] audiobgmArr;
    private AudioSource bgmAudioSource;
    private int bgmCount;
    private int currentIndex;

    private Coroutine playSoundRoutine;

    public void Init()
    {
        bgmAudioSource = transform.Find("BGMAudio").GetComponent<AudioSource>();
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
        bgmAudioSource.Stop();
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
            if (!bgmAudioSource.isPlaying)
            {
                bgmAudioSource.clip = audiobgmArr[currentIndex];
                bgmAudioSource.Play();
                currentIndex++;
            }
        }
    }
}
