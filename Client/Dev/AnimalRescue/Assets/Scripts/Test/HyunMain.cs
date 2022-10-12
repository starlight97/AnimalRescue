using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HyunMain : MonoBehaviour
{
    private Player player;
    private EnemySpawner enemySpawner;
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

        this.player = GameObject.FindObjectOfType<Player>();

        this.player.onUpdateMove = (worldPos) => 
        {
            this.uiGame.uiHpGauge.UpdatePosition(worldPos);
        };

        this.player.Init();
        this.player.onHit = (hp, maxHp) =>
        {
            this.uiGame.uiHpGauge.DecreaseHp(hp, maxHp);
        };

        this.player.onDie = () =>
        {

        };

        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            var data = DataManager.instance.GetData<WeaponData>(2002);
            shootingStar.Init(data);
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
    }
}