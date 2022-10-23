using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLife
{
    private float hp;
    private float maxHp;

    public float MaxHp { get; set; }

    public float Hp
    {
        get { return hp; }
        set 
        { 
            if (value <= 0)
            {
                this.hp = 0;
            }
            else
            {
                this.hp = value;
            }
        }
    }
}
