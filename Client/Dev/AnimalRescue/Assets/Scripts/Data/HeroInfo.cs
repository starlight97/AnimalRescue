using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroInfo
{
    public int id;
    public int level;

    public HeroInfo(int id, int level = 1)
    {
        this.id = id;
        this.level = level;
    }
}
