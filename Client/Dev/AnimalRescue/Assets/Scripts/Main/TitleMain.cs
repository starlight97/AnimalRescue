using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMain : SceneMain
{
    private UITitle uiTitle;
    public override void Init(SceneParams param = null)
    {
        this.uiTitle = GameObject.FindObjectOfType<UITitle>();

        this.uiTitle.onClickBtn = (type) =>
        {
            if(type == "GameReady")
            {
                Dispatch("onClickGameReady");
            }
            else if (type == "Shop")
            {
                Dispatch("onClickShop");
            }
        };
        this.uiTitle.Init();
    }


}
