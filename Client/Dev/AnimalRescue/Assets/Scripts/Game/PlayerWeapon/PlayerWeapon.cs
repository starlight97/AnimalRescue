using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int id;
    protected int projectile_current_count;
    protected int current_damage;
    protected float current_attack_speed;
    protected int level;

    protected WeaponData weaponData;

    virtual public void Init(WeaponData weaponData)
    {
        this.weaponData = weaponData;
        this.projectile_current_count = 1;
        this.current_damage = weaponData.damage;
        this.current_attack_speed = weaponData.attack_speed;
        this.level = 1;
    }

    virtual public void Init(WeaponData weaponData, Transform trans)
    {
        Init(weaponData);
    }

    // 기준으로 빙빙 도는 무기 구현 하고 싶을때 쓰세요 
    // trans : 빙빙돌 기준점, circleR : 반지름
    virtual public void Init(WeaponData weaponData, Transform trans, float circleR)
    {
        Init(weaponData);
        //this.weaponData = weaponData;
        //this.projectile_current_count = 1;
        //this.level = 1;
    }

    virtual public void Upgrade()
    {
        
        this.level++;
        // 투사체 최대 갯수 도달시 부터는 damage가 증가
        // 데미지 공식 = 무기 고유공격력 * 무기레벨
        if(projectile_current_count >= this.weaponData.projectile_count)
        {
            // 10 -> - 10 -> 0 
            this.current_damage = (int)((this.level - (this.weaponData.projectile_count - 1)) * this.weaponData.increase_damage_per) * weaponData.damage;
            Debug.Log(this.current_damage);
            //this.weaponData.damage = (this.level - (this.weaponData.projectile_count - 1)) * this.weaponData.damage;
        }
    }
}