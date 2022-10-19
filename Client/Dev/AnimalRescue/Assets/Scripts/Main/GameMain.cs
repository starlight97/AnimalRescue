using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            this.uiGame.UpdatePosition(worldPos);
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
        this.uiGame.onWeaponSelect = (id) =>
        {
            this.Resume();
            this.weaponManager.WeaponUpgrade(id);
            Debug.Log(id + " : Level Up 선택!~@!@~");
        };



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
