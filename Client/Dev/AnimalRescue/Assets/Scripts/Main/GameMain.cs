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
            var info = InfoManager.instance.GetInfo();
            info.playerInfo.gold += getGold;
            // 씬이동 게임오버씬으로
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

        this.uiGame.Init();
        this.player.Init(gameMainParam.heroId);
        this.enemySpawner.Init();
        this.waveManager.Init();
        this.weaponManager.Init(2000);

        DataManager.instance.onDataLoadFinished.AddListener(() => 
        {

        });
        
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

}
