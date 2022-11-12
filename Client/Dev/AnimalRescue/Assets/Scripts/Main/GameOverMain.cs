using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMain : SceneMain
{
    private UIGameOver uiGameOver;


    public override void Init(SceneParams param = null)
    {
        base.Init(param);
        GameOverMainParam gameOvermainParam = (GameOverMainParam)param;
        this.uiGameOver = GameObject.FindObjectOfType<UIGameOver>();

        this.uiGameOver.onClickBtn = (btnName) => 
        {
            RecordManager.instance.ResetScore();
            Dispatch("onClick" + btnName);
        };

        this.uiGameOver.Init(gameOvermainParam.heroId);

        var playerInfo = InfoManager.instance.GetInfo().playerInfo;

        var gold = RecordManager.instance.GetGold();
        var enemyCount = RecordManager.instance.GetEnemyCount();
        var wave = RecordManager.instance.GetWave();
        var playTime = RecordManager.instance.GetPlayTime();

        RecordManager.instance.UpdateHighRecordWave();
        RecordManager.instance.UpdateHighRecordTime();

        this.uiGameOver.GetRecordText(enemyCount, gold, playTime, wave);
        this.uiGameOver.GetHighRecord(playerInfo.highRecordWave, playerInfo.highRecordTime);
    }
}
