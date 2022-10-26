using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo 
{
    public string gpgsid;
    public PlayerInfo playerInfo;
    public Dictionary<int, HeroInfo> dicHeroInfo;

    public GameInfo(string gpgsid)
    {
        this.gpgsid = gpgsid;
        this.playerInfo = new PlayerInfo(500000, 500000);
        this.dicHeroInfo = new Dictionary<int, HeroInfo>();
    }
}
