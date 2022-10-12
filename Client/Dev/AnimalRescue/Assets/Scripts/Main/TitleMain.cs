using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : SceneMain
{
    public override void Init(SceneParams param = null)
    {
        //StartCoroutine(this.TouchToStartRoutine());
        StartCoroutine(this.WaitForClick());
    }

    private IEnumerator WaitForClick()
    {
        yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

        this.StopAllCoroutines();

        this.Dispatch("onClick");
    }
}
