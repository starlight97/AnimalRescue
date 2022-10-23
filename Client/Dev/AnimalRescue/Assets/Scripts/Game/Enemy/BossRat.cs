using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRat : Enemy
{
    public GameObject bossRatDoughnutPrefab;
    public float radius;
    private int projectileCount;


    public override void Init(int maxHp, int damage, int experience, float movespeed, float attackspeed)
    {
        base.Init(maxHp, damage, experience, movespeed, attackspeed);
        projectileCount = 10;
        switch (Level)
        {
            case 1:
                projectileCount = 1;
                break;
            default:
                break;
        }
    }

    protected override IEnumerator AttackRoutine()
    {
        RadiusAttack();
        yield return new WaitForSeconds(this.attackspeed);
        this.attackRoutine = null;
    }

    private void RadiusAttack()
    {
        float degree = this.transform.rotation.y;
        var rot = this.transform.rotation;
        rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        var angle = rot.eulerAngles.y;

        SpawnBullet(angle, degree);

        int cnt = 1;
        int cnt2 = 1;
        while (true)
        {
            
            if (cnt2 >= projectileCount)
                break;
            if (cnt2 < projectileCount)
            {
                cnt2++;
                SpawnBullet(angle + (cnt * 20), degree);
            }
            if (cnt2 < projectileCount)
            {
                cnt2++;
                SpawnBullet(angle - (cnt * 20), degree);
            }
            cnt++;
        }


    }

    private void SpawnBullet(float angle, float degree)
    {
        var radian = degree * Mathf.PI / 180;
        var x = Mathf.Cos(radian) * radius;
        var z = Mathf.Sin(radian) * radius;
        var pos = new Vector3(x, 0, z);
        var bossRatDoughnutGo = Instantiate<GameObject>(this.bossRatDoughnutPrefab);

        // 회전축 , 회전값    
        var rot = Quaternion.Euler(0, angle, 0);

        bossRatDoughnutGo.transform.rotation = rot;
        bossRatDoughnutGo.transform.position = pos + this.transform.position;
        var bossRatDoughnu = bossRatDoughnutGo.GetComponent<BossRatDoughnut>();
        bossRatDoughnu.Init(this.damage);
    }
}
