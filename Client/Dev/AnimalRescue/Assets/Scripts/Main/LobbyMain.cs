using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMain : SceneMain
{
    private UILobby uiLobby;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.uiLobby = GameObject.FindObjectOfType<UILobby>();

        this.uiLobby.onClickBtn = (type) =>
        {
            switch (type)
            {
                case UILobby.eBtnLobby.GameReady:
                    Dispatch("onClickGameReady");
                    break;
                case UILobby.eBtnLobby.Shop:
                    Dispatch("onClickShop");
                    break;
                case UILobby.eBtnLobby.Option:
                    Debug.Log("Option");
                    break;
                case UILobby.eBtnLobby.Exit:
                    Application.Quit();
                    break;

            }
        };
        this.uiLobby.Init();
    }
}
