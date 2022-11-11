using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class TitleMain : SceneMain
{
    private UITitle uiTitle;
    public AudioClip btnAudio;
    public AudioClip[] bgmlist;

    public override void Init(SceneParams param = null)
    {
        //base.Init();
        this.onDestroy.AddListener(() =>
        {
            SoundManager.instance.StopBGMSound();
        });
        SoundManager.instance.PlayBGMSound(bgmlist);

        this.uiTitle = GameObject.Find("UITitle").GetComponent<UITitle>();
        //StartCoroutine(this.TouchToStartRoutine());
        StartCoroutine(this.WaitForClick());
        this.uiTitle.Init();
    }
        

    private IEnumerator WaitForClick()
    {        
        var uiTitleHeroGo = GameObject.Find("UITitleHero");
        var uiTitleHero = uiTitleHeroGo.GetComponent<UITitleHero>();
        uiTitleHero.Init();
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
        SoundManager.instance.PlaySound(btnAudio);
        uiTitleHero.RunAnimation();
        this.StopAllCoroutines();

        this.Dispatch("onClick");
    }
}