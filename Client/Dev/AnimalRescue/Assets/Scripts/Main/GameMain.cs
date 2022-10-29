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

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

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
        this.enemySpawner.onDieEnemy = (experience) =>
        {
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
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
        this.player.Init(102);
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
