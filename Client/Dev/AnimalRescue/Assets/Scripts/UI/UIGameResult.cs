using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameResult : MonoBehaviour
{
    private Text enemyText;
    private Text goldText;
    private Text timeText;
    private Text waveText;

    private Text highWaveText;
    private Text highTimeText;

    private Image newWaveImg;
    private Image newTimeImg;

    public void Init()
    {
        enemyText = GameObject.Find("EnemyText").GetComponent<Text>();
        goldText = GameObject.Find("GoldText").GetComponent<Text>();
        timeText = GameObject.Find("PlayTimeText").GetComponent<Text>();
        waveText = GameObject.Find("WaveText").GetComponent<Text>();
        highWaveText = GameObject.Find("HighWaveText").GetComponent<Text>();
        highTimeText = GameObject.Find("HighPlayTimeText").GetComponent<Text>();

        newWaveImg = GameObject.Find("NewWaveIcon").GetComponent<Image>();
        newTimeImg = GameObject.Find("NewTimeIcon").GetComponent<Image>();

        newWaveImg.gameObject.SetActive(false);
        newTimeImg.gameObject.SetActive(false);
    }

    public void SetResultText(int enemy, int gold, string time, int wave)
    {
        enemyText.text = string.Format("{0}", enemy);
        goldText.text = string.Format("{0}", gold);
        timeText.text = time;
        waveText.text = string.Format("{0}", wave);
    }

    public void SetHighRecord(int highWave, string highTime)
    {
        highWaveText.text = string.Format("{0}", highWave);
        highTimeText.text = highTime;
    }

    public void ShowNewWaveIcon()
    {
        Debug.Log("dd");
        bool checkNewWave = RecordManager.instance.CheckNewWaveRecord();
        bool checkNewTime = RecordManager.instance.CheckNewTimeRecord();
        if (checkNewWave)
            newWaveImg.gameObject.SetActive(true);
        if (checkNewTime)
            newTimeImg.gameObject.SetActive(true);
    }
}
