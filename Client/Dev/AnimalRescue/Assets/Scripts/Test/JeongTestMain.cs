using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JeongTestMain : MonoBehaviour
{
    public Enemy enemy;
    public Player player;
    float delta = 0f;
    private void Start()
    {
        enemy.Init(1,1,1,1,1,1,10);
        player.Init(100);
    }

    private void Update()
    {
        delta += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeSpan t = TimeSpan.FromSeconds(delta);

            string answer = string.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                            t.Hours,
                            t.Minutes,
                            t.Seconds,
                            t.Milliseconds);
            Debug.Log("delta : " + delta);
            Debug.Log("answer : " + answer);
        }
    }


}
