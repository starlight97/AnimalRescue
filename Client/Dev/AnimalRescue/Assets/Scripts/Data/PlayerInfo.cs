using System;
public class PlayerInfo 
{
    public int gold;
    public int diamond;
    public string highRecordtime;
    public int highRecordStage;

    public PlayerInfo(int gold, int diamond,int highRecordStage, string highRecordtime = "0")
    {
        this.gold = gold;
        this.diamond = diamond;
        this.highRecordStage = highRecordStage;
        this.highRecordtime = highRecordtime;
    }
}
