using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameStatus : MonoBehaviour
{
    private Text playTime;
    private Text wave;
    public Text enemyCountText;
    public Text goldCountText;

    public void Init()
    {
        this.playTime = this.transform.Find("PlayTime").transform.Find("TimeText").GetComponent<Text>();
        this.wave = this.transform.Find("Wave").transform.Find("WaveText").GetComponent<Text>();

        this.playTime.text = string.Format("{0:D2}:{1:D2}", 0, 0);
        this.wave.text = string.Format("WAVE {0}", 0);
        this.enemyCountText.text = string.Format("{0}", 0);
        this.goldCountText.text = string.Format("{0}", 0);
    }

    public void SetProgressText(int enemyCount, int goldCount)
    {
        enemyCountText.text = enemyCount.ToString();
        goldCountText.text = goldCount.ToString();
    }

    public void SetWaveText(int currentWave)
    {
        this.wave.text = string.Format("WAVE {0}", currentWave.ToString());
    }

    public void SetPlayTimeText(string time)
    {
        this.playTime.text = time;
    }
}
