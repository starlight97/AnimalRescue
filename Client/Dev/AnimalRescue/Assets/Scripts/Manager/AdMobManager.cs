using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdMobManager : MonoBehaviour
{
    private string adUnitId;
    private RewardedAd shopCoinRewardedAd;
    private RewardedAd gameOverRewardedAd;

    public static AdMobManager instance;

    public System.Action<Reward> onHandleUserEarnedReward;
    public System.Action<AdFailedToLoadEventArgs> onHandleRewardedAdFailedToLoad;

    public System.Action onHandleRewardedAdFailedToShow;
    public System.Action onHandleRewardedAdClosed;

    private void Awake()
    {
        instance = this;
    }


    public void Init(string adUnitId)
    {
        //adUnitId 설정
#if UNITY_EDITOR
        //string adUnitId = "unused";
        this.shopCoinRewardedAd = CreateAndLoadRewardedAd(adUnitId);
        this.gameOverRewardedAd = CreateAndLoadRewardedAd(adUnitId);
#elif UNITY_ANDROID
        string adUnitId = "ca-app-pub-4572742510387968/2132883982";
#elif UNITY_IPHONE
        string adUnitId = "";
#else
        string adUnitId = "unexpected_platform";
#endif

        // 모바일 광고 SDK를 초기화함. 
        MobileAds.Initialize(initStatus => {
            // 광고를 요청하기 전에 각 어댑터의 초기화 상태를 확인
            // 광고 로드 전 초기화 완료될 때까지 기다려야함.
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;
                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        MonoBehaviour.print("Adapter: " + className + " is initialized.");
                        break;
                }
            }
        });

        ////광고 로드 : RewardedAd 객체의 loadAd메서드에 AdRequest 인스턴스를 넣음
        //AdRequest request = new AdRequest.Builder().Build();
        //this.rewardedAd = new RewardedAd(adUnitId);
        //this.rewardedAd.LoadAd(request);


        //this.rewardedAd.OnAdLoaded += HandleRewardedAdLoaded; // 광고 로드가 완료되면 호출
        //this.rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad; // 광고 로드가 실패했을 때 호출
        //this.rewardedAd.OnAdOpening += HandleRewardedAdOpening; // 광고가 표시될 때 호출(기기 화면을 덮음)
        //this.rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow; // 광고 표시가 실패했을 때 호출
        //this.rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;// 광고를 시청한 후 보상을 받아야할 때 호출
        //this.rewardedAd.OnAdClosed += HandleRewardedAdClosed; // 닫기 버튼을 누르거나 뒤로가기 버튼을 눌러 동영상 광고를 닫을 때 호출
    }
    public void HandleRewardedAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdLoaded");
    }

    public void HandleRewardedAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToLoad");
        this.onHandleRewardedAdFailedToLoad(args);

    }

    public void HandleRewardedAdOpening(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdOpening");
    }

    public void HandleRewardedAdFailedToShow(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdFailedToShow");
        this.onHandleRewardedAdFailedToShow();
    }

    public void HandleRewardedAdClosed(object sender, EventArgs args)
    {
        Debug.Log("HandleRewardedAdClosed");
        this.onHandleRewardedAdClosed();
    }

    public void HandleUserEarnedReward(object sender, Reward args)
    {
        Debug.Log("HandleUserEarnedReward");
        this.onHandleUserEarnedReward(args);

    }

    public RewardedAd CreateAndLoadRewardedAd(string adUnitId)
    {
        RewardedAd rewardedAd = new RewardedAd(adUnitId);
        rewardedAd.OnAdLoaded += HandleRewardedAdLoaded; // 광고 로드가 완료되면 호출
        rewardedAd.OnAdFailedToLoad += HandleRewardedAdFailedToLoad; // 광고 로드가 실패했을 때 호출
        rewardedAd.OnAdOpening += HandleRewardedAdOpening; // 광고가 표시될 때 호출(기기 화면을 덮음)
        rewardedAd.OnAdFailedToShow += HandleRewardedAdFailedToShow; // 광고 표시가 실패했을 때 호출
        rewardedAd.OnUserEarnedReward += HandleUserEarnedReward;// 광고를 시청한 후 보상을 받아야할 때 호출
        rewardedAd.OnAdClosed += HandleRewardedAdClosed; // 닫기 버튼을 누르거나 뒤로가기 버튼을 눌러 동영상 광고를 닫을 때 호출

        // Create an empty ad request.
        AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded ad with the request.
        rewardedAd.LoadAd(request);
        return rewardedAd;
    }

    #region ShopCoin

    public bool IsShopCoinLoaded()
    {
        return this.shopCoinRewardedAd.IsLoaded();
    }

    public void ShowShopCoinAds()
    {
        StartCoroutine(this.ShowShopCoinAdsRoutine());
    }

    private IEnumerator ShowShopCoinAdsRoutine()
    {
        while (true)
        {
            bool check = IsShopCoinLoaded();
            if (check == true)
            {
                this.shopCoinRewardedAd.Show();
                break;
            }
            else
            {
                Debug.Log("reward ad not loaded.");
            }

            yield return null;
        }
    }
    #endregion

    #region GameOver

    public bool IsGameOverLoaded()
    {
        return this.gameOverRewardedAd.IsLoaded();
    }

    public void ShowGameOverAds()
    {
        StartCoroutine(this.ShowGameOverAdsRoutine());
    }

    private IEnumerator ShowGameOverAdsRoutine()
    {
        while (true)
        {
            bool check = IsGameOverLoaded();
            if (check == true)
            {
                this.gameOverRewardedAd.Show();
                break;
            }
            else
            {
                Debug.Log("reward ad not loaded.");
            }

            yield return null;
        }
    }
    #endregion
}
