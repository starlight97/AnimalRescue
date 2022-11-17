using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public AudioClip bossSpawnAudio;
    public AudioClip[] bossBgmAudioArr;
    public UnityAction<int, int> onDieEnemy;
    public UnityAction<int, int> onDieBoss;
    private Vector3[] spawnPoints;
    private int spawnCount;
    private List<EnemyData> enemyDataList;
    private List<BossData> bossDataList;

    private int randEnemyCount = 2;
    //private List<EnemyBossData> enemyBossDataList;
    public List<Enemy> EnemyList
    {
        get;
        private set;
    }

    public void Init()
    {
        this.spawnPoints = new Vector3[4];
        this.EnemyList = new List<Enemy>();
        this.spawnPoints[0] = new Vector3(73, 0, -73);
        this.spawnPoints[1] = new Vector3(-73, 0, -73);
        this.spawnPoints[2] = new Vector3(-73, 0, 73);
        this.spawnPoints[3] = new Vector3(73, 0, 73);
        this.enemyDataList = new List<EnemyData>();
        this.bossDataList = new List<BossData>();
        this.enemyDataList = DataManager.instance.GetDataList<EnemyData>().ToList();
        this.bossDataList = DataManager.instance.GetDataList<BossData>().ToList();
    }

    public void StartWave(int wave)
    {
        SpawnEnemy(wave);

        if (wave % 5 == 0)
        {
            this.SpawnBoss(wave);
        }

    }

    private void SpawnEnemy(int wave)
    {
        StartCoroutine(this.SpawnEnemyRoutine(wave));
    }

    private IEnumerator SpawnEnemyRoutine(int wave)
    {
        this.spawnCount = 0;
        List<EnemyData> tempEnemyDataList = enemyDataList.ToList();
        while (true)
        {
            var randIdx = Random.Range(0, tempEnemyDataList.Count);
            tempEnemyDataList.RemoveAt(randIdx);

            if (tempEnemyDataList.Count <= randEnemyCount)
                break;
        }

        while (true)
        {
            if (spawnCount == GameConstants.SpawnEnemyCount)
                break;

            var pos = this.GetRandomPos();
            var randIdx = Random.Range(0, tempEnemyDataList.Count);
            int experience = GameConstants.EnemyExperience;
            int level = wave;

            GameObject enemyGo = Instantiate(Resources.Load<GameObject>(tempEnemyDataList[randIdx].prefab_name),pos, Quaternion.identity);
            enemyGo.transform.parent = this.transform;
            Enemy enemy = enemyGo.GetComponent<Enemy>();
            EnemyList.Add(enemy);
            enemy.Init(level, tempEnemyDataList[randIdx].max_hp, tempEnemyDataList[randIdx].damage,
    experience, tempEnemyDataList[randIdx].move_speed, tempEnemyDataList[randIdx].attack_speed, tempEnemyDataList[randIdx].attack_range);

            enemy.onDie = (dieEnemy) =>
            {
                EnemyList.Remove(dieEnemy);
                this.onDieEnemy(dieEnemy.id, dieEnemy.experience);
                Destroy(dieEnemy.gameObject);
            };
            spawnCount++;
            
            yield return new WaitForSeconds(GameConstants.EnemySpawnTime);
        }
    }

    private void SpawnBoss(int wave)
    {
        SoundManager.instance.PlaySound(bossSpawnAudio);
        SoundManager.instance.StopBGMSound();
        SoundManager.instance.PlayBGMSound(bossBgmAudioArr);
        var pos = this.GetRandomPos();
        var randIdx = Random.Range(0, bossDataList.Count);
        int experience = GameConstants.EnemyExperience * 33;
        int level = wave / 5;
        GameObject bossGo = Instantiate(Resources.Load<GameObject>(bossDataList[randIdx].prefab_name), pos, Quaternion.identity);
        bossGo.transform.parent = this.transform;
        Enemy enemy = bossGo.GetComponent<Enemy>();
        EnemyList.Add(enemy);
        enemy.Init(level, bossDataList[randIdx].max_hp, bossDataList[randIdx].damage, 
            experience, bossDataList[randIdx].move_speed, bossDataList[randIdx].attack_speed, bossDataList[randIdx].attack_range);
        enemy.onDie = (dieEnemy) =>
        {
            EnemyList.Remove(dieEnemy);
            this.onDieBoss(dieEnemy.id, dieEnemy.experience);
            Destroy(dieEnemy.gameObject);
        };
    }

    private Vector3 GetRandomPos()
    {
        // randPoint1 = 0 ~ 3
        // randPoint2 = ex( randPoint1가 3이라면 spawnPoints[3] ~ spawnPoints[0] ) (73, 0, 73) ~ (73, 0, -73)
        // randPoint2 = ex( randPoint1가 2라면 spawnPoints[2] ~ spawnPoints[1] ) (-73, 0, 73) ~ (-73, 0, -73)
        int randPoint1 = Random.Range(0, 4);
        int randPoint2;
        if (randPoint1 == 0)
            randPoint2 = 3;
        else
            randPoint2 = randPoint1 - 1;

        Vector3 pos = new Vector3();
        pos.x = Random.Range(spawnPoints[randPoint1].x, spawnPoints[randPoint2].x);
        pos.y = 0;
        pos.z = Random.Range(spawnPoints[randPoint1].z, spawnPoints[randPoint2].z);

        return pos;
    }

}
