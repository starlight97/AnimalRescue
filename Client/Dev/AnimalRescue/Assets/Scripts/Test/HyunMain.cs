using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyunMain : MonoBehaviour
{
    private Player player;
    private EnemySpawner enemySpawner;
    private WaveManager waveManager;
    public BasicWeapon basicWeapon;
    public ShootingStar shootingStar;
    public UIGame uiGame;
    

    void Start()
    {
        Init();
    }

    public void Init()
    {
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        this.enemySpawner.Init(10);

        this.waveManager = GameObject.FindObjectOfType<WaveManager>();

        this.player = GameObject.FindObjectOfType<Player>();

        this.player.onUpdateMove = (worldPos) => 
        {
            this.uiGame.uiHpGauge.UpdatePosition(worldPos);
        };

        this.player.onHit = (hp, maxHp) =>
        {
            this.uiGame.uiHpGauge.DecreaseHp(hp, maxHp);
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
            var data = DataManager.instance.GetData<WeaponData>(2002);
            //shootingStar.Init(data);
            enemySpawner.Init(30);
            this.player.Init();
            waveManager.Init();
        });

        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            basicWeapon.Upgrade();
        }
        if (Input.GetKeyDown(KeyCode.X))
            shootingStar.Upgrade();
    }
}