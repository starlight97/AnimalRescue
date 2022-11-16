using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var info = InfoManager.instance.GetInfo();

        // 점수 불러오기 및 갱신
        var gold = RecordManager.instance.GetGold();
        var enemyCount = RecordManager.instance.GetEnemyCount();
        var wave = RecordManager.instance.GetWave();
        var playTime = RecordManager.instance.GetPlayTime();

        RecordManager.instance.UpdateHighRecordWave();
        RecordManager.instance.UpdateHighRecordTime();

        this.uiGameOver.GetRecordText(enemyCount, gold, playTime, wave);
        this.uiGameOver.GetHighRecord(info.playerInfo.highRecordWave, info.playerInfo.highRecordTime);

        bool checkHeroOpen = RecordManager.instance.IsNewHeroOpen();

        if (checkHeroOpen)
        {
            this.uiGameOver.ShowPopup();
        }
    }
}
