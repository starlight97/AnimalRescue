using System;
public class PlayerInfo 
{
    public int gold;
    public int diamond;
    public string highrecordtime;

    public PlayerInfo(int gold, int diamond, string highrecordtime="0")
    {
        this.gold = gold;
        this.diamond = diamond;
        this.highrecordtime = highrecordtime;
    }
}
