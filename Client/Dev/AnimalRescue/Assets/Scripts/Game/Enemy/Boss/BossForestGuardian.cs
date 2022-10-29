using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForestGuardian : Enemy
{
    public GameObject cubePrefab;
    public float cubeMaxScale;
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
            diffusionRate = 0.2f;
        }
        else if (this.level == 2)
        {
            diffusionRate = 0.4f;
        }
        else if (this.level == 3)
        {
            diffusionRate = 0.6f;
        }
        else if (this.level >= 4)
        {
            diffusionRate = 0.8f;
        }
    }
}
