using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordManager 
{
    public static readonly RecordManager instance = new RecordManager();

    private int gold = 0;
    private int enemyCount = 0;
    private int waveCount = 0;
    private string playtime;

    private bool isNewTimeRecord = false;
    private bool isNewWaveRecord = false;
    private bool isNewHeroOpen = false;

    public int GetGold()
    {
        return gold;
    }

    public void AddGold(int getGold)
    {
        gold += getGold;
    }
    
    public int GetEnemyCount()
    {
        return enemyCount;
    }

    public void AddEnemyCount(int killEnemy)
    {
        enemyCount += killEnemy;
    }


    public string GetPlayTime()
    {
        return playtime;
    }

    public void SetPlayTime(string time)
    {
        playtime = time;
    }

    public int GetWave()
    {
        return waveCount;
    }

    public void SetWave(int wave)
    {
        waveCount = wave;
    }

    public void ResetScore()
    {
        gold = 0;
        enemyCount = 0;
        waveCount = 0;
        playtime = null;
    }

    public void UpdateHighRecordWave()
    {
        var info = InfoManager.instance.GetInfo().playerInfo;
        if (waveCount > info.highRecordWave)
        {
            info.highRecordWave = waveCount;
            InfoManager.instance.SaveGame();
            this.isNewWaveRecord = true;
        }
        else 
            return;
    }

    public void UpdateHighRecordTime()
    {
        var info = InfoManager.instance.GetInfo().playerInfo;

        var highRecordTime = TimeSpan.Parse(info.highRecordTime).TotalSeconds;
        
        double currentTime = TimeSpan.Parse(playtime).TotalSeconds;
        
        if (currentTime > highRecordTime)
        {
            highRecordTime = currentTime;
            TimeSpan t = TimeSpan.FromSeconds(highRecordTime);
            info.highRecordTime = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                        t.Hours,
                                        t.Minutes,
                                        t.Seconds);
            InfoManager.instance.SaveGame();
            isNewTimeRecord = true;
        }
        else
            return;
    }

    public bool CheckNewTimeRecord()
    {
        return isNewTimeRecord;
    }

    public bool CheckNewWaveRecord()
    {
        return isNewWaveRecord;
    }

    public void SaveKillEnemy(Dictionary<int, int> dicKillEnemy)
    {
        var dicKillEnemyInfo = InfoManager.instance.GetInfo().dicKillEnemyInfo;

        foreach (var killEnemy in dicKillEnemy)
        {
            if(dicKillEnemyInfo.ContainsKey(killEnemy.Key) == false)
            {
                dicKillEnemyInfo.Add(killEnemy.Key, killEnemy.Value);
                if(killEnemy.Value >= GameConstants.NEWHEROUNLOCKVALUE)
                {
                    isNewHeroOpen = true;
                }
            }
            else
            {
                if (dicKillEnemyInfo[killEnemy.Key] <= GameConstants.NEWHEROUNLOCKVALUE &&
                    dicKillEnemyInfo[killEnemy.Key] + killEnemy.Value >= GameConstants.NEWHEROUNLOCKVALUE)
                {
                    isNewHeroOpen = true;
                }
                dicKillEnemyInfo[killEnemy.Key] += killEnemy.Value;
                if (dicKillEnemyInfo[killEnemy.Key] >= 50000)
                    dicKillEnemyInfo[killEnemy.Key] = 50000;
            }

        }
    }

    public bool IsNewHeroOpen()
    {
        bool check = isNewHeroOpen;
        isNewHeroOpen = false;
        return check;
    }
}
