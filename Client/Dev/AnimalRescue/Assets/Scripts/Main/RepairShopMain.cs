using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShopMain : SceneMain
{
    private UIRepairShop uiRepairShop;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

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

        this.OptionInit();
        this.uiRepairShop.Init(mainParam.heroId);

    }


}
