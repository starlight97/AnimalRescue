using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongTestMain : MonoBehaviour
{
    private Player player;
    private EnemySpawner enemySpawner;
    private WaveManager waveManager;
    public UIJeongTest uiJeongTest;
    private bool isPause = false;

    public CottonCandy cottonCandy;
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
            this.uiJeongTest.uiHpGauge.UpdatePosition(worldPos);
        };
        this.player.onHit = (n1, n2) =>
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
        this.uiJeongTest.onWeaponSelect = (id) =>
        {
            this.Resume();
            Debug.Log(id + " : Level Up 선택!~@!@~");
        };


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
            this.uiJeongTest.Init();
        });
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);


    }

    private void GameObjectSetting()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();
        this.waveManager = GameObject.FindObjectOfType<WaveManager>();
        this.uiJeongTest = GameObject.FindObjectOfType<UIJeongTest>();
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
