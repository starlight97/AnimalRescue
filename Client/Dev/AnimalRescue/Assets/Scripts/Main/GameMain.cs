using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameMain : SceneMain
{
    private Player player;
    private EnemySpawner enemySpawner;
    private WaveManager waveManager;
    private WeaponManager weaponManager;
    private UIGame uiGame;

    private int getGold = 0;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        GameMainParam gameMainParam = (GameMainParam)param;

        GameObjectSetting();

        this.player.onLevelUp = (amount) =>
        {
            Pause();
            uiGame.ShowWeaponLevelUp();
            Debug.Log("레벨업!");
        };
        this.player.onUpdateMove = (worldPos) =>
        {
            this.uiGame.FixedHpGaugePosition(worldPos);
        };
        this.player.onUpdateHp = (hp, maxHp) =>
        {
            this.uiGame.UpdateUIHpGauge(hp, maxHp);
        };
        this.player.onDie = () =>
        {
            Pause();
            this.uiGame.ShowRivivePanel();
            this.uiGame.RunTimer();

            //// 씬이동 게임오버씬으로
        };
        this.enemySpawner.onDieEnemy = (experience) =>
        {
            getGold += 1;
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
        };
        this.enemySpawner.onDieBoss = (experience) =>
        {
            getGold += 10;
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
            waveManager.StartWave();
        };
        this.waveManager.onWaveStart = (wave) =>
        {
            Debug.Log(wave + " : wave start");
            enemySpawner.StartWave(wave);
        };
        this.uiGame.onWeaponSelect = (id) =>
        {
            this.Resume();
            this.weaponManager.WeaponUpgrade(id);
            Debug.Log(id + " : Level Up 선택!~@!@~");
        };
        this.uiGame.onGameOver = () => 
        {
            Resume();
            var info = InfoManager.instance.GetInfo();
            info.playerInfo.gold += getGold;
            Dispatch("onGameOver");
        };
        this.uiGame.onClickAds = () =>
        {
            ShowAds();
        };

        this.uiGame.Init();
        this.player.Init(gameMainParam.heroId);
        this.enemySpawner.Init();
        this.waveManager.Init();
        //this.weaponManager.Init(2000);
    }

    private void GameObjectSetting()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        this.waveManager = GameObject.FindObjectOfType<WaveManager>();
        this.uiGame = GameObject.FindObjectOfType<UIGame>();
        this.weaponManager = GameObject.FindObjectOfType<WeaponManager>();
    }
    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void ShowAds()
    {
        AdMobManager.instance.Init("ca-app-pub-3940256099942544/5224354917");
        // 진짜 광고!!!!!!!!! 바꿔~~~~~!!!!!!!!!!!!!!!!!!
        //AdMobManager.instance.Init("ca-app-pub-4572742510387968/3117467058");

        AdMobManager.instance.ShowGameOverAds();
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
            player.playerLife.Hp = (float)(player.playerLife.MaxHp * (reward.Amount) / 100);
        };
    }

}
