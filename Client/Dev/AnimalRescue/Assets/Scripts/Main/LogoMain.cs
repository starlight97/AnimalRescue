using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoMain : SceneMain
{
    public override void Init(SceneParams param = null)
    {
        this.useOnDestoryEvent = false;

        StartCoroutine(this.ShowLogoRoutine());
    }

    private IEnumerator ShowLogoRoutine()
    {
        yield return new WaitForSeconds(2f);

        this.Dispatch("onShowLogoComplete");
    }
}
