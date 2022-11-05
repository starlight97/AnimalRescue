using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMain : SceneMain
{
    private UIGameOver uiGameOver;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);
        GameOverMainParam gameOvermainParam = (GameOverMainParam)param;
        this.uiGameOver = GameObject.FindObjectOfType<UIGameOver>();

        this.uiGameOver.onClickBtn = (btnName) => 
        {
            Dispatch("onClick" + btnName);
        };

        this.uiGameOver.Init();
    }
}
