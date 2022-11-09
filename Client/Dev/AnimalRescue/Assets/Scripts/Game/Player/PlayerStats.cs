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
    public float moveSpeed;
    private int experience;

    public void Init(int id, int experience)
    {
        var heroData = DataManager.instance.GetData<HeroData>(id);
        var info = InfoManager.instance.GetInfo();

        this.damage = heroData.damage + info.dicHeroInfo[heroData.id].dicStats["damage"] * heroData.increase_damage;
        this.maxHp = (int)(heroData.max_hp + info.dicHeroInfo[heroData.id].dicStats["maxhp"] * heroData.increase_maxhp);
        this.moveSpeed = heroData.move_speed + (info.dicHeroInfo[heroData.id].dicStats["movespeed"] * heroData.increase_movespeed) / 10;
        this.experience = experience;
    }

    // 경험치 획득 메소드
    public void GetExp(int experience)
    {
        this.experience += experience;
        if (this.experience >= GameConstants.RequiredExperience)
        {            
            this.LevelUp();
        }
    }

    // 레벨업 했을시 플레이어한테 알려준다.
    // 한번에 200이상의 경험치를 얻었을 경우엔 레벨업을 몇번 해야 하는지 알아야 하므로
    // int amount = this.experience / 100; 적용
    public void LevelUp()
    {
        int amount = this.experience / GameConstants.RequiredExperience;
        this.experience %= GameConstants.RequiredExperience;
        this.onLevelUp(amount);
    }
}
