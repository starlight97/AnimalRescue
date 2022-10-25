using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFloorboard : PlayerWeapon
{
    private float healAmount = 0.1f;
    private Player player;
    float currentHp;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        player = GameObject.FindObjectOfType<Player>();
        currentHp = player.playerLife.Hp;
        Heal();
    }

    public void Heal()
    {
        StartCoroutine(HealRoutine());
    }

    private IEnumerator HealRoutine()
    {
        while (true)
        {
            currentHp += healAmount;
            player.playerLife.Hp = currentHp;
            if (player.playerLife.Hp >= player.playerLife.MaxHp)
                player.playerLife.Hp = player.playerLife.MaxHp;
            yield return new WaitForSeconds(1f);
        }
    }


    // 힐량 0.05씩 증가
    public override void Upgrade()
    {
        //base.Upgrade();
        this.level++;
        this.healAmount += 0.05f;
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }
}
