using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossElementalGolem : Enemy
{
    public GameObject stonePrefab;
    public float radius;
    private int projectileCount = 0;

    public override void Init(int level, int maxHp, int damage, int experience, float movespeed, float attackspeed, float attackRange)
    {
        base.Init(level, maxHp, damage, experience, movespeed, attackspeed, attackRange);

        this.DifficultySetting();
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
        var bossRatDoughnutGo = Instantiate<GameObject>(this.stonePrefab);

        // 회전축 , 회전값    
        var rot = Quaternion.Euler(0, angle, 0);

        bossRatDoughnutGo.transform.rotation = rot;
        bossRatDoughnutGo.transform.position = pos + this.transform.position;
        var bossRatDoughnu = bossRatDoughnutGo.GetComponent<BossElementalGolemStone>();
        bossRatDoughnu.Init(this.damage);
    }

    protected override void DifficultySetting()
    {
        base.DifficultySetting();

        if (this.level == 1)
        {
            projectileCount = 2;
        }
        else if (this.level == 2)
        {
            projectileCount = 4;
        }
        else if (this.level == 3)
        {
            projectileCount = 6;
        }
        else if (this.level >= 4)
        {
            projectileCount = 8;
        }
    }
}
