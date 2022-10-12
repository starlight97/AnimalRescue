using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonCandyProjectile : PlayerProjectile
{

    public override void Init(int damage)
    {
        base.Init(damage);
    }

    public override void Attack(Collider collider)
    {
        base.Attack(collider);
    }

    protected override IEnumerator MoveRoutine()
    {
        while(true)
        {
            yield return null;
        }
    }




}
