using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMain : SceneMain
{
    private UIShop uiShop;
    public AudioClip[] bgmlist;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);
        this.onDestroy.AddListener(() =>
        {
            SoundManager.instance.StopBGMSound();
        });
        SoundManager.instance.PlayBGMSound(bgmlist);

        this.uiShop = (UIShop)this.uiBase;
        this.uiShop.Init();
        

        this.uiShop.onClickLobby = () =>
        {
            Dispatch("onClickLobby");
        };

        this.uiShop.onClickAdsBtn = () => 
        {
            ShowAds();
        };

        this.uiShop.onDataLoadComplete = () =>
        {
            Dispatch("onReload");
        };

        this.OptionInit();
    }

    private void ShowAds()
    {
        AdMobManager.instance.Init();
        AdMobManager.instance.ShowShopCoinAds();
        AdMobManager.instance.onHandleRewardedAdClosed = () => {
            // 로딩창 제거
            Debug.Log("onHandleRewardedAdClosed");
        };
        AdMobManager.instance.onHandleRewardedAdFailedToLoad = (args) => {
            // 로딩창 제거
            Debug.LogFormat("onHandleRewardedAdFailedToLoad: {0}", args.LoadAdError.ToString());
        };
        AdMobManager.instance.onHandleRewardedAdFailedToShow = () => {
            // 로딩창 제거
            Debug.LogFormat("onHandleRewardedAdFailedToShow");
        };
        AdMobManager.instance.onHandleUserEarnedReward = (reward) => {
            // 보상 주기
            Debug.LogFormat("{0} {1}", reward.Type, reward.Amount);
            var info = InfoManager.instance.GetInfo();
            info.playerInfo.gold += (int)reward.Amount;
            InfoManager.instance.SaveGame();

            var shopTextGold = this.uiShop.GetTextGold();
            shopTextGold.text = info.playerInfo.gold.ToString();
        };
    }

}
