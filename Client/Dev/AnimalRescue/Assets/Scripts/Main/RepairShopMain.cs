using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShopMain : SceneMain
{
    private UIRepairShop uiRepairShop;
    public AudioClip[] bgmlist;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);
        this.onDestroy.AddListener(() =>
        {
            SoundManager.instance.StopBGMSound();
        });
        SoundManager.instance.PlayBGMSound(bgmlist);
        var mainParam = (RepairShopParam)param;

        this.uiRepairShop = (UIRepairShop)this.uiBase;


        this.uiRepairShop.onClickBtn = (type) =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
            switch (type)
            {
                case UIRepairShop.eBtnRepairShop.Back:
                    Dispatch("onClickLobby");
                    break;
                case UIRepairShop.eBtnRepairShop.Shop:
                    Dispatch("onClickShop");
                    break;
            }            
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

        this.OptionInit();
        this.uiRepairShop.Init(mainParam.heroId);

    }


}
