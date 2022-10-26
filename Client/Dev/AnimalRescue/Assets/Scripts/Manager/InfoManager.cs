using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InfoManager 
{
    public static readonly InfoManager instance = new InfoManager();

    private GameInfo gameInfo;

    // path에 데이터 파일이 있을경우 기존유저 처리해야함 true 반환
    // 없을경우 신규유저 처리해야함 false 반환

    public void Init()
    {
        this.LoadData();
    }
    private void LoadData()
    {
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);

        if (File.Exists(path))
        {
            Debug.Log("기존 유저");
            var json = File.ReadAllText(path);
            this.gameInfo = JsonConvert.DeserializeObject<GameInfo>(json);
            //datas.ToDictionary(x => x.gpgsid).ToList().ForEach(x => dicInfos.Add(x.Key, x.Value));
        }
        else
        {
            Debug.Log("신규 유저 입니다.");
            gameInfo = new GameInfo("testid");
            HeroInfo heroInfo = new HeroInfo(100);
            gameInfo.dicHeroInfo.Add(heroInfo.id, heroInfo);
            HeroInfo heroInfo1 = new HeroInfo(101);
            gameInfo.dicHeroInfo.Add(heroInfo1.id, heroInfo1);
            HeroInfo heroInfo2 = new HeroInfo(102);
            gameInfo.dicHeroInfo.Add(heroInfo2.id, heroInfo2);
        }
        SaveGame();
    }

    public void SaveGame()
    {
        var json = JsonConvert.SerializeObject(this.gameInfo);
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        File.WriteAllText(path, json);
    }

    public GameInfo GetInfo()
    {
        return this.gameInfo;
    }
}
