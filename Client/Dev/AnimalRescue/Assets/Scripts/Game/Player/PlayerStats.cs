using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class PlayerStats : MonoBehaviour
{    
    //public float CoolTimeDecreasePer;

    public UnityAction<int> onLevelUp;
    public int damage;
    public float maxHp;
    public int moveSpeed;
    private int experience;

    //public void Init(int damage, float coolTimeDecreasePer, int experience)
    //{
    //    this.Damage = damage;
    //    this.CoolTimeDecreasePer = coolTimeDecreasePer;
    //    this.experience = experience;
    //}

    public void Init(int damage, float maxHp, int moveSpeed, int experience)
    {
        this.damage = damage;
        this.maxHp = maxHp;
        this.moveSpeed = moveSpeed;
        this.experience = experience;
    }

    // 경험치 획득 메소드
    public void GetExp(int experience)
    {
        this.experience += experience;
        if (this.experience >= StatsConstants.RequiredExperience)
        {            
            this.LevelUp();
        }
    }

    // 레벨업 했을시 플레이어한테 알려준다.
    // 한번에 200이상의 경험치를 얻었을 경우엔 레벨업을 몇번 해야 하는지 알아야 하므로
    // int amount = this.experience / 100; 적용
    public void LevelUp()
    {
        int amount = this.experience / StatsConstants.RequiredExperience;
        this.experience %= StatsConstants.RequiredExperience;
        this.onLevelUp(amount);
    }

}
