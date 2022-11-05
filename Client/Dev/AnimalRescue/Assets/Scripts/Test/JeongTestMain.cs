using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongTestMain : MonoBehaviour
{
    private Player player;
    private EnemySpawner enemySpawner;
    private WaveManager waveManager;
    private WeaponManager weaponManager;
    public UIJeongTest uiJeongTest;
    public Enemy boss;

    // Start is called before the first frame update
    void Start()
    {
        //this.PlayerCenter.GetComponent<Player>();
        GameObjectSetting();

        this.player.onLevelUp = (amount) =>
        {
            Pause();
            uiJeongTest.ShowWeaponLevelUp();
            Debug.Log("레벨업!");
        };
        this.player.onUpdateMove = (worldPos) =>
        {
            this.uiJeongTest.UpdatePosition(worldPos);
        };
        this.player.onUpdateHp = (hp, maxHp) =>
        {
            this.uiJeongTest.UpdateUIHpGauge(hp, maxHp);
        };
        this.enemySpawner.onDieEnemy = (experience) =>
        {
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
        };
        this.enemySpawner.onDieBoss = (experience) =>
        {
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
            waveManager.StartWave();
        };
        this.waveManager.onWaveStart = (wave) =>
        {
            Debug.Log(wave + " : wave start");
            enemySpawner.StartWave(wave);
        };
        this.uiJeongTest.onWeaponSelect = (id) =>
        {
            this.Resume();
            this.weaponManager.WeaponUpgrade(id);
            Debug.Log(id + " : Level Up 선택!~@!@~");
        };


        DataManager.instance.onDataLoadComplete.AddListener((n1, n2) =>
        {

        });
        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            //this.enemySpawner.Init();
            this.uiJeongTest.Init();
            this.player.Init(100);
            //this.waveManager.TestInit();
            this.boss.Init(1, 10000,10,10,5,1, 5);
            this.weaponManager.Init(2000);
            //uiJeongTest.ShowWeaponLevelUp();
        });
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);
        InfoManager.instance.Init();


    }

    private void GameObjectSetting()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        this.waveManager = GameObject.FindObjectOfType<WaveManager>();
        this.uiJeongTest = GameObject.FindObjectOfType<UIJeongTest>();
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
