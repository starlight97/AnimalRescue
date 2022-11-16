using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyMain : SceneMain
{
    private UILobby uiLobby;
    public int selectedHeroId;
    public GameObject heroViewGo;
    public AudioClip[] bgmlist;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);
        this.onDestroy.AddListener(() =>
        {
            SoundManager.instance.StopBGMSound();
        });

        this.uiLobby = (UILobby)this.uiBase;
        SoundManager.instance.PlayBGMSound(bgmlist);
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
                    this.heroViewGo.SetActive(false);
                    break;
                case UILobby.eBtnLobby.OptionClose:
                    this.heroViewGo.SetActive(true);
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

            var info = InfoManager.instance.GetInfo();
            int damage = (heroData.damage +info.dicHeroInfo[selectedHeroId].dicStats["damage"] * heroData.increase_damage);
            int maxhp = (int)(heroData.max_hp + info.dicHeroInfo[selectedHeroId].dicStats["maxhp"] * heroData.increase_maxhp);
            float movespeed = (heroData.move_speed + info.dicHeroInfo[selectedHeroId].dicStats["movespeed"] * heroData.increase_movespeed);

            this.uiLobby.UiLobbyHeroStatsUIUpdate(heroData.hero_name,damage,maxhp, movespeed);

            if(heroViewGo.transform.childCount > 0)
                Destroy(heroViewGo.transform.GetChild(0).gameObject);
            var uiHeroGo = Instantiate(Resources.Load<GameObject>(heroData.ui_prefab_path), heroViewGo.transform);
 
        };


        GPGSManager.instance.onSavedCloud = () =>
        {
            //this.textGameInfo.text = status.ToString();
        };
        GPGSManager.instance.onLoadedCloud = (info) =>
        {
            InfoManager.instance.SetInfo(info);
            Dispatch("onReload");
            //var json = JsonConvert.SerializeObject(this.gameInfo);
        };
        GPGSManager.instance.onErrorHandler = (status) =>
        {
            Debug.Log("ERROR : " + status);
        };

        this.uiLobby.Init();
        this.OptionInit();
    }
}
