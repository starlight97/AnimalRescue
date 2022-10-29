using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossForestGuardian : Enemy
{
    public GameObject cubePrefab;
    public float cubeMaxScale;
     
    protected override IEnumerator AttackRoutine()
    {
        var cubeGo = Instantiate<GameObject>(cubePrefab, this.transform.position, Quaternion.identity);
        var cube = cubeGo.GetComponent<BossBadgerCube>();
        cube.Init(this.damage, cubeMaxScale);
        cube.onAttackComplete = () =>
        {
            attackRoutine = null;
        };

        yield return null;
    }
}
