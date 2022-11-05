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


        this.uiRepairShop.onClickLobby = () =>
        {
            Dispatch("onClickLobby");
        };

        this.optionManager.Init();
        this.uiRepairShop.Init(mainParam.heroId);
        this.soundManager.Init();
        this.soundManager.PlayBGMSound();
    }


}
