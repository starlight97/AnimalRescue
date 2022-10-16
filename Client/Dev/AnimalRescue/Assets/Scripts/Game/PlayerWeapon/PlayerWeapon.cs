using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int id;
    public int projectile_current_count;
    public int current_damage;
    public float current_attack_speed;
    public int level;

    protected WeaponData weaponData;
    protected Transform playerTrans;

    virtual public void Init(WeaponData weaponData, Transform playerTrans)
    {
        this.weaponData = weaponData;
        this.projectile_current_count = 1;
        this.current_damage = weaponData.damage;
        this.current_attack_speed = weaponData.attack_speed;
        this.level = 1;
        this.playerTrans = playerTrans;
        this.StartCoroutine(this.FollowPlayerRoutine());
    }

    virtual public void Upgrade()
    {        
        this.level++;
        // 투사체 최대 갯수 도달시 부터는 damage가 증가
        // 데미지 공식 = 무기 고유공격력 * 무기레벨
        if(projectile_current_count >= this.weaponData.projectile_max_state)
        {
            // 10 -> - 10 -> 0 
            this.current_damage = (int)((this.level - (this.weaponData.projectile_max_state - 1)) * this.weaponData.increase_damage_per) * weaponData.damage;
            //this.weaponData.damage = (this.level - (this.weaponData.projectile_count - 1)) * this.weaponData.damage;
        }
    }

    public IEnumerator FollowPlayerRoutine()
    {
        while (true)
        {
            this.transform.position = playerTrans.position;
            yield return null;
        }
    }
}
