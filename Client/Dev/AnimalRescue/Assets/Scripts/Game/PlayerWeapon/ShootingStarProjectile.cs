using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarProjectile : PlayerProjectile
{
    public GameObject particlePrefab;
    private GameObject particleGo;

    public override void Init(int damage, float moveSpeed, Vector3 dir)
    {
        base.Init(damage, moveSpeed, dir);
    }

    public override void Attack(Collider collider)
    {
        base.Attack(collider);
        if (collider.CompareTag("Enemy"))
        {
            Debug.Log("크아아아아아앗");
            Destroy(this.gameObject);
        }
    }

    public void CreateParticle(Transform trans)
    {
        this.particleGo = Instantiate<GameObject>(particlePrefab);
        this.particleGo.transform.position = trans.position;

        if (this.transform.position.y <= -2)
        {
            Debug.Log("밍야옹");
            Destroy(this.particleGo.gameObject);
        }
    }
}
