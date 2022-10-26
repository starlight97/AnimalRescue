using System.Collections;
using System.Collections.Generic;
public class HeroInfo
{
    // 이건 레벨임
    public Dictionary<string, int> dicStats;
    public int id;
    //public int maxhp;
    //public int damage;
    //public int movespeed;
    //public int defense;
    //public int cooltime;
    //public int evasionrate;

    public HeroInfo(int id)
    {
        dicStats = new Dictionary<string, int>();
        this.id = id;
        dicStats.Add("maxhp",1);
        dicStats.Add("damage", 1);
        dicStats.Add("movespeed", 1);
        dicStats.Add("defense", 1);
        dicStats.Add("cooltime", 1);
        dicStats.Add("evasionrate", 1);
    }
}
