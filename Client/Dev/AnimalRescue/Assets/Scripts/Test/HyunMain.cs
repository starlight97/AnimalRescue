using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyunMain : MonoBehaviour
{
    private Player player;
    private EnemySpawner enemySpawner;
    private WaveManager waveManager;
    private WeaponManager weaponManager;
    public UIGame uiGame;

    void Start()
    {
        Init();
    }

    public void Init()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        this.waveManager = GameObject.FindObjectOfType<WaveManager>();
        this.weaponManager = GameObject.FindObjectOfType<WeaponManager>();


        this.enemySpawner.Init(10);

        this.player.onUpdateMove = (worldPos) => 
        {
            //this.uiGame.uiHpGauge.UpdatePosition(worldPos);
        };

        this.player.onHit = (hp, maxHp) =>
        {
            //this.uiGame.uiHpGauge.DecreaseHp(hp, maxHp);
        };

        this.player.onDie = () =>
        {

        };

        this.enemySpawner.onDieEnemy = (experience) =>
        {
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
        };

        this.waveManager.onWaveStart = (wave) =>
        {
            enemySpawner.StartWave(wave);
        };


        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            enemySpawner.Init(30);
            player.Init();
            waveManager.Init();
            weaponManager.Init(2005);
        });

        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.X))
            //this.weaponManager.WeaponUpgrade(2004);
    }
}