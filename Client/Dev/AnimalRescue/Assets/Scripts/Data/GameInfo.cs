using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo 
{
    public string gpgsid;
    PlayerInfo playerInfo;

    public GameInfo(string gpgsid)
    {
        this.gpgsid = gpgsid;
        this.playerInfo = new PlayerInfo(500, 500);
    }
}
