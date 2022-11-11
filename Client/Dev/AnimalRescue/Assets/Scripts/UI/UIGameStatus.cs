using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIGameStatus : MonoBehaviour
{
    private Text playTime;
    private Text wave;
    public Text enemyCountText;
    public Text goldCountText;

    public UnityAction onSetProgressComplete;

    public void Init()
    {
        this.playTime = this.transform.Find("PlayTime").transform.Find("TimeText").GetComponent<Text>();
        this.wave = this.transform.Find("Wave").transform.Find("WaveText").GetComponent<Text>();
    }

    public void SetProgressText(int enemyCount, int goldCount)
    {
        enemyCountText.text = enemyCount.ToString();
        goldCountText.text = goldCount.ToString();
    }

    public void SetWaveText(int currentWave)
    {
        this.wave.text = currentWave.ToString();
    }

    public void SetPlayTIme(float delta)
    {

    }
}
