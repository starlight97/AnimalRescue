using System;
public class PlayerInfo 
{
    public int gold;
    public int diamond;
    public string highRecordTime;
    public int highRecordWave;

    public PlayerInfo(int gold, int diamond,int highRecordWave, string highRecordTime = "00:00:00")
    {
        this.gold = gold;
        this.diamond = diamond;
        this.highRecordWave = highRecordWave;
        this.highRecordTime = highRecordTime;
    }
}
