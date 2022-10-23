using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int id;
    public int current_damage;
    public float current_attack_speed;
    public int level;

    protected WeaponData weaponData;
    protected Transform playerTrans;

    virtual public void Init(WeaponData weaponData, Transform playerTrans)
    {
        this.weaponData = weaponData;
        this.current_damage = weaponData.damage;
        this.current_attack_speed = weaponData.attack_speed;
        this.level = 1;
        this.playerTrans = playerTrans;
    }

    virtual public void Upgrade()
    {        
        this.level++;
        this.current_damage = (int)((this.level * this.weaponData.increase_damage_per) * weaponData.damage);
    }

    protected void FollowPlayer()
    {
        var newPos = playerTrans.position;
        newPos.y = 0.001f;
        this.transform.position = newPos;
    }
}
