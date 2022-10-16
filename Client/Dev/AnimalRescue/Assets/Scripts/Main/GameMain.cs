using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : SceneMain
{
    private Player player;
    private EnemySpawner enemySpawner;
    private WaveManager waveManager;
    public UIGame uiGame;
    private bool isPause = false;

    public CottonCandy cottonCandy;
    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        GameObjectSetting();

        this.player.onLevelUp = (amount) =>
        {
            Pause();
            Debug.Log("레벨업!");
        };
        this.player.onUpdateMove = (worldPos) =>
        {
            this.uiGame.uiHpGauge.UpdatePosition(worldPos);
        };
        this.player.onHit = (hp, maxHp) =>
        {
            
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



    }
    // Start is called before the first frame update
    void Start()
    {
        this.Init();

        DataManager.instance.onDataLoadComplete.AddListener((n1, n2) =>
        {

        });
        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            var data = DataManager.instance.GetData<WeaponData>(2001);
            cottonCandy.Init(data, player.transform, 5);
            this.enemySpawner.Init(30);
            this.player.Init();
            this.waveManager.Init();
        });
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);
    }

    private void GameObjectSetting()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        this.waveManager = GameObject.FindObjectOfType<WaveManager>();
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        isPause = false;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isPause = true;
    }

}
