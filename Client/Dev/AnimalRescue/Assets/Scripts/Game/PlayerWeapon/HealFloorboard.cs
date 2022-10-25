using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFloorboard : PlayerWeapon
{
    private float amount = 0.1f;
    private Player player;
    float currentHp;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        player = GameObject.FindObjectOfType<Player>();
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
            player.Recovery(player.playerLife.Hp, player.playerLife.MaxHp, amount);
            yield return new WaitForSeconds(1f);
        }
    }


    // 힐량 0.05씩 증가
    public override void Upgrade()
    {
        //base.Upgrade();
        this.level++;
        this.amount += 0.5f;
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }
}
