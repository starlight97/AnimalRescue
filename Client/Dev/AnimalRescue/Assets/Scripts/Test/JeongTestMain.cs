using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongTestMain : MonoBehaviour
{
    private Player player;
    private EnemySpawner enemySpawner;
    private bool isPause = false;

    public CottonCandy cottonCandy;
    // Start is called before the first frame update
    void Start()
    {
        //this.PlayerCenter.GetComponent<Player>();
        GameObjectSetting();

        //this.player.onLevelUp = (amount) =>
        //{
        //    Pause();
        //    Debug.Log("레벨업!");
        //};
        this.player.onHit = (n1, n2) =>
        {

        };
        this.enemySpawner.onDieEnemy = (experience) =>
        {
            PlayerStats playerStats = this.player.GetComponent<PlayerStats>();
            playerStats.GetExp(experience);
        };


        DataManager.instance.onDataLoadComplete.AddListener((n1, n2) =>
        {

        });
        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            var data = DataManager.instance.GetData<WeaponData>(2001);
            cottonCandy.Init(data, player.transform, 5);
        });
        DataManager.instance.Init();
        DataManager.instance.LoadAllData(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (isPause == true)
                Resume();

            cottonCandy.Upgrade();
        }
    }

    private void GameObjectSetting()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();

        this.player.Init();
        this.enemySpawner.Init(50);
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
