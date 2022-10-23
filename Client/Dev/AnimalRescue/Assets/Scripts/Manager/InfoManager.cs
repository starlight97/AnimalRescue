using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class InfoManager 
{
    public static readonly InfoManager instance = new InfoManager();

    private Dictionary<string, GameInfo> dicInfos = new Dictionary<string, GameInfo>();

    // path에 데이터 파일이 있을경우 기존유저 처리해야함 true 반환
    // 없을경우 신규유저 처리해야함 false 반환
    public void LoadData()
    {
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);

        if (File.Exists(path))
        {
            Debug.Log("기존 유저");
            var json = File.ReadAllText(path);
            var datas = JsonConvert.DeserializeObject<GameInfo[]>(json);
            datas.ToDictionary(x => x.gpgsid).ToList().ForEach(x => dicInfos.Add(x.Key, x.Value));
        }
        else
        {
            Debug.Log("신규 유저 입니다.");
            GameInfo gameInfo = new GameInfo("testid");
            dicInfos.Add(gameInfo.gpgsid, gameInfo);
        }
        SaveGame();
    }

    public void SaveGame()
    {
        var json = JsonConvert.SerializeObject(this.dicInfos.Values);
        var path = string.Format("{0}/game_info.json", Application.persistentDataPath);
        File.WriteAllText(path, json);
    }
}
