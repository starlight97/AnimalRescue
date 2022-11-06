using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : SceneMain
{
    private UITitle uiTitle;

    public override void Init(SceneParams param = null)
    {
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

        uiTitleHero.RunAnimation();
        this.StopAllCoroutines();

        this.Dispatch("onClick");
    }
}