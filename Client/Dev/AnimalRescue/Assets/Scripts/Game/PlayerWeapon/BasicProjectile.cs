using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProjectile : PlayerProjectile
{
    public override void Init(int damage, float moveSpeed, Vector3 dir)
    {
        base.Init(damage, moveSpeed, dir);
    }


    public override void Attack(Collider collider)
    {
        base.Attack(collider);
        Destroy(this.gameObject);
    }
}