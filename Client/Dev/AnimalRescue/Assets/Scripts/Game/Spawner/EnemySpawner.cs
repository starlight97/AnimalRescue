using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawner : MonoBehaviour
{
    public UnityAction<int> onDieEnemy;
    public float spawnDelay;
    public GameObject[] enemyPrefabs;
    private Vector3[] spawnPoints;
    private int spawnCount;
    private int maxSpawnCount;
    public List<Enemy> EnemyList
    {
        get;
        private set;
    }

    public void Init(int maxSpawnCount)
    {
        this.spawnPoints = new Vector3[4];
        this.EnemyList = new List<Enemy>();
        this.spawnPoints[0] = new Vector3(73, 0, -73);
        this.spawnPoints[1] = new Vector3(-73, 0, -73);
        this.spawnPoints[2] = new Vector3(-73, 0, 73);
        this.spawnPoints[3] = new Vector3(73, 0, 73);
        this.spawnCount = 0;
        this.maxSpawnCount = maxSpawnCount;
        SpawnEnemy();
    }

    private void SpawnEnemy()
    {

        StartCoroutine(this.SpawnEnemyRoutine());
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while(true)
        {
            if (spawnCount == maxSpawnCount)
                break;
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

            GameObject enemyGo = Instantiate<GameObject>(enemyPrefabs[0]);
            Enemy enemy = enemyGo.GetComponent<Enemy>();
            EnemyList.Add(enemy);
            enemyGo.transform.position = pos;
            enemy.Init(100, 1);
            enemy.onDie = (dieEnemy) =>
            {
                EnemyList.Remove(dieEnemy);
                this.onDieEnemy(dieEnemy.experience);
                Destroy(dieEnemy.gameObject);
            };
            spawnCount++;

            yield return new WaitForSeconds(spawnDelay);
        }
    }

}
