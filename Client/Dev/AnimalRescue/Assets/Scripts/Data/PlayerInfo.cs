using System;
public class PlayerInfo 
{
    public int gold;
    public int diamond;
    public string highRecordtime;
    public int highRecordWave;

    public PlayerInfo(int gold, int diamond,int highRecordWave, string highRecordtime = "0")
    {
        this.gold = gold;
        this.diamond = diamond;
        this.highRecordWave = highRecordWave;
        this.highRecordtime = highRecordtime;
    }
}
