using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyunMain : MonoBehaviour
{
    private Player player;
    private EnemySpawner enemySpawner;
    private WaveManager waveManager;
    private WeaponManager weaponManager;
    private UIHyunTest uiHyunTest;

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
        this.uiHyunTest = GameObject.FindObjectOfType<UIHyunTest>();

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
            uiHyunTest.Init();
            player.Init(105);
            enemySpawner.Init();
            waveManager.Init();
            weaponManager.Init(2004);
        });


        this.player.onUpdateMove = (worldPos) =>
        {
            this.uiHyunTest.UpdatePosition(worldPos);
        };

        this.player.onUpdateHp = (hp, maxHp) =>
        {
            this.uiHyunTest.UpdateUIHpGauge(hp, maxHp);
        };

        this.player.onDie = () =>
        {

        };
        InfoManager.instance.Init();
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
            this.weaponManager.WeaponUpgrade(2004);
    }
}