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
    private PlayTimeManager playTime;
    private Dictionary<int, int> dicKillEnemy;

    //public int GetGold { get; private set ; }
    //public int KillEnemy { get; private set; }

    public AudioClip[] bgmlist;
    public AudioClip levelUpAudio;

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.dicKillEnemy = new Dictionary<int, int>();

        this.onDestroy.AddListener(() =>
        {
            SoundManager.instance.StopBGMSound();
        });

        SoundManager.instance.PlayBGMSound(bgmlist);

        this.uiGame = (UIGame)this.uiBase;
        this.playTime = GameObject.FindObjectOfType<PlayTimeManager>();

        GameMainParam gameMainParam = (GameMainParam)param;

        GameObjectSetting();

        this.player.onLevelUp = (amount) =>
        {
            Pause();
            uiGame.ShowWeaponLevelUp();
            SoundManager.instance.PlaySound(levelUpAudio);
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
        this.player.onGiveRiviveChance = () =>
        {
            // 광고 패널 띄우기
            Pause();
            this.uiGame.ShowRivivePanel(true);
            this.uiGame.RunTimer();
        };

        this.player.onDie = () =>
        {
            this.weaponManager.gameObject.SetActive(false);
            this.uiGame.ShowRivivePanel(false);
            var info = InfoManager.instance.GetInfo();
            var getGold = RecordManager.instance.GetGold();

            RecordManager.instance.SaveKillEnemy(this.dicKillEnemy);

            info.playerInfo.gold += getGold;
            InfoManager.instance.SaveGame();
            SoundManager.instance.StopSound();
            Dispatch("onGameOver");
        };
        #region #####################################################

        this.enemySpawner.onDieEnemy = (enemyid, experience) =>
        {
            if (dicKillEnemy.ContainsKey(enemyid) == false)
            {
                dicKillEnemy.Add(enemyid, 1);
            }
            else
            {
                dicKillEnemy[enemyid]++;
            }

            RecordManager.instance.AddGold(1);
            RecordManager.instance.AddEnemyCount(1);

            var getGold = RecordManager.instance.GetGold();
            var enemyCount = RecordManager.instance.GetEnemyCount();

            this.uiGame.SetProgress(enemyCount, getGold);
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
        };
        this.enemySpawner.onDieBoss = (enemyid, experience) =>
        {
            RecordManager.instance.AddGold(10);
            RecordManager.instance.AddEnemyCount(1);
            var getGold = RecordManager.instance.GetGold();
            var enemyCount = RecordManager.instance.GetEnemyCount();
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
            this.uiGame.SetProgress(enemyCount, getGold);
            waveManager.StartWave();

            SoundManager.instance.StopBGMSound();
            SoundManager.instance.PlayBGMSound(bgmlist);
        };
        this.waveManager.onWaveStart = (wave) =>
        {
            Debug.Log(wave + " : wave start");
            enemySpawner.StartWave(wave);
            RecordManager.instance.SetWave(wave);
            this.uiGame.SetWave(wave);
        };
        this.uiGame.onWeaponSelect = (id) =>
        {
            this.Resume();
            this.weaponManager.WeaponUpgrade(id);
        };
        this.playTime.onPassesTime = (time) => 
        {
            RecordManager.instance.SetPlayTime(time);
            this.uiGame.SetPlayTime(time);
        };
        #endregion
        this.uiGame.onGameOver = () => 
        {
            Resume();

            this.uiGame.ShowRivivePanel(false);
            var enemys = this.enemySpawner.EnemyList;
            foreach (var enemy in enemys)
            {
                enemy.StopAllCoroutines();
            }
            this.player.SetDieState(true);
        };
        this.uiGame.onClickAds = () =>
        {
            ShowAds();
            player.SetRiviveState(true);
        };
        this.uiGame.btnGameExit.onClick.AddListener(() =>
        {
            Resume();
            var info = InfoManager.instance.GetInfo();
            var getGold = RecordManager.instance.GetGold();

            RecordManager.instance.SaveKillEnemy(this.dicKillEnemy);

            info.playerInfo.gold += getGold;
            InfoManager.instance.SaveGame();
            SoundManager.instance.StopSound();
            Dispatch("onGameExit");
        });

        this.uiGame.Init();
        this.player.Init(gameMainParam.heroId);
        this.enemySpawner.Init();
        this.waveManager.Init();
        this.weaponManager.Init(2000);
        this.playTime.Init();
        this.OptionInit();

    }

    private void GameObjectSetting()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        this.waveManager = GameObject.FindObjectOfType<WaveManager>();
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
        AdMobManager.instance.Init("ca-app-pub-4572742510387968/3117467058");

        AdMobManager.instance.ShowGameOverAds();
        AdMobManager.instance.onHandleRewardedAdClosed = () => {
            Resume();
            this.uiGame.HiderotateDots();
            this.uiGame.ShowRivivePanel(false);
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
            player.playerLife.Hp = (float)(player.playerLife.MaxHp * (reward.Amount) / 100);
            this.uiGame.UpdateUIHpGauge(player.playerLife.Hp, player.playerLife.MaxHp);
        };
    }
}
