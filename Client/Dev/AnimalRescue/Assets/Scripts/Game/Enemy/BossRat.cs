using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRat : Enemy
{
    public GameObject bossRatDoughnutPrefab;
    public float radius;
    private int projectileCount;

    public override void Init(int level, int maxHp, int damage, int experience, float movespeed, float attackspeed, float attackRange)
    {
        base.Init(level, maxHp, damage, experience, movespeed, attackspeed, attackRange);
        projectileCount = 10;
        switch (this.level)
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
        yield return new WaitForSeconds(this.attackSpeed);
        this.attackRoutine = null;
    }

    private void RadiusAttack()
    {
        float degree = this.transform.rotation.y;
        var rot = this.transform.rotation;
        rot = Quaternion.Euler(0, rot.eulerAngles.y, 0);
        var angle = rot.eulerAngles.y;

        SpawnBullet(angle, degree);

        int angleCount = 1;
        int currentProjectileCount = 1;
        while (true)
        {            
            if (currentProjectileCount >= projectileCount)
                break;
            if (currentProjectileCount < projectileCount)
            {
                currentProjectileCount++;
                SpawnBullet(angle + (angleCount * 20), degree);
            }
            if (currentProjectileCount < projectileCount)
            {
                currentProjectileCount++;
                SpawnBullet(angle - (angleCount * 20), degree);
            }
            angleCount++;
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
