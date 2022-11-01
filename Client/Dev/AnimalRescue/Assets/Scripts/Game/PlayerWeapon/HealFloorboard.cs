using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFloorboard : PlayerWeapon
{
    private float per = 1.01f;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
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
            var player = playerTrans.GetComponent<Player>();
            player.Recovery(player.playerLife.Hp, player.playerLife.MaxHp, per);
            yield return new WaitForSeconds(1f);
        }
    }


    // 힐량 0.1퍼씩 증가
    public override void Upgrade()
    {
        //base.Upgrade();
        this.level++;
        this.per += 0.05f;
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }
}
