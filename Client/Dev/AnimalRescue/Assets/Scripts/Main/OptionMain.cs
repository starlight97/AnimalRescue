using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionMain : SceneMain
{
    private UIOption uiOption;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.uiOption = GameObject.FindObjectOfType<UIOption>();

        this.uiOption.Init();

    }
}
