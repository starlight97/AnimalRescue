using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public int id;
    public int current_damage;
    public float current_attack_speed;
    public int level;
    public int playerDamage;

    protected WeaponData weaponData;
    protected Transform playerTrans;

    virtual public void Init(WeaponData weaponData, Transform playerTrans)
    {
        this.playerTrans = playerTrans;
        this.playerDamage = playerTrans.GetComponent<PlayerStats>().damage;
        this.weaponData = weaponData;
        this.current_damage = weaponData.damage + playerDamage;
        this.current_attack_speed = weaponData.attack_speed;
        this.level = 1;
    }


    virtual public void Upgrade()
    {        
        this.level++;
        this.current_damage += (int)(this.weaponData.damage * this.weaponData.increase_damage_per );
    }

    protected void FollowPlayer()
    {
        var newPos = playerTrans.position;
        newPos.y = 0.001f;
        this.transform.position = newPos;
    }
}
