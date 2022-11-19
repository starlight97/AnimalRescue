using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForestGuardian : Enemy
{
    public GameObject cubePrefab;
    public float cubeMaxScale;    // 큐브 크기
    public float diffusionRate;   // 큐브 확산속도

    public override void Init(int level, int maxHp, int damage, int experience, float movespeed, float attackspeed, float attackRange)
    {
        base.Init(level, maxHp, damage, experience, movespeed, attackspeed, attackRange);

        this.DifficultySetting();
    }

    protected override IEnumerator AttackRoutine()
    {
        var cubeGo = Instantiate<GameObject>(cubePrefab, this.transform.position, Quaternion.identity);
        var cube = cubeGo.GetComponent<BossForestGuardianCube>();
        cube.Init(this.damage, cubeMaxScale, diffusionRate);
        cube.onAttackComplete = () =>
        {
            attackRoutine = null;
        };

        yield return null;
    }

    protected override void DifficultySetting()
    {
        base.DifficultySetting();

        if (this.level == 1)
        {
            cubeMaxScale = 15f;
            diffusionRate = 0.03f;
        }
        else if (this.level == 2)
        {
            diffusionRate = 0.06f;
        }
        else if (this.level == 3)
        {
            cubeMaxScale = 20f;
            diffusionRate = 0.1f;
        }
        else if (this.level >= 4)
        {
            diffusionRate = 0.13f;
        }
    }
}
