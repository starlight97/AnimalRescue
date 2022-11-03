using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairShopMain : SceneMain
{
    private UIRepairShop uiRepairShop;
    private SoundManager soundManager;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        var mainParam = (RepairShopParam)param;

        this.uiRepairShop = GameObject.FindObjectOfType<UIRepairShop>();
        this.soundManager = GameObject.FindObjectOfType<SoundManager>();

        this.uiRepairShop.onClickLobby = () =>
        {
            Dispatch("onClickLobby");
        };

        this.uiRepairShop.Init(mainParam.heroId);
        this.soundManager.Init();
        this.soundManager.PlayBGMSound();
    }


}
