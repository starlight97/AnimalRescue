using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMain : SceneMain
{
    private EnemySpawner enemySpawner;
    // Start is called before the first frame update
    void Start()
    {
        enemySpawner = GameObject.FindObjectOfType<EnemySpawner>();

        enemySpawner.Init(10);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
