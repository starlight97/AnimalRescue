using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMain : SceneMain
{
    private UILobby uiLobby;
    public int selectedHeroId = 100;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.uiLobby = GameObject.FindObjectOfType<UILobby>();

        this.uiLobby.onClickBtn = (type) =>
        {
            switch (type)
            {
                case UILobby.eBtnLobby.GameStart:
                    Dispatch("onClickGameStart");
                    break;
                case UILobby.eBtnLobby.Shop:
                    Dispatch("onClickShop");
                    break;
                case UILobby.eBtnLobby.RepairShop:                    
                    Dispatch("onClickRepairShop");
                    break;
                case UILobby.eBtnLobby.Option:
                    Debug.Log("Option");
                    break;
                case UILobby.eBtnLobby.Exit:
                    Application.Quit();
                    break;

            }
        };
        
        this.uiLobby.onClickHero = (id) =>
        {
            selectedHeroId = id;

            var heroData = DataManager.instance.GetData<HeroData>(id);

            this.uiLobby.UiLobbyHeroStatsUIUpdate(heroData.hero_name,1,1,1);
        };
        this.uiLobby.Init();
    }
}
