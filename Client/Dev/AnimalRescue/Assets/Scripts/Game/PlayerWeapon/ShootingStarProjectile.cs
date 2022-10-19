using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShootingStarProjectile : PlayerProjectile
{
    public GameObject particlePrefab;
    private GameObject particleGo;
    public UnityAction onScaleUpComplete;


    public override void Init(int damage, float moveSpeed, Vector3 dir)
    {
        base.Init(damage, moveSpeed, dir);
    }

    public override void Attack(Collider collider)
    {
        base.Attack(collider);
        if (collider.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    public void CreateParticle(Transform trans, Color col)
    {
        this.particleGo = Instantiate<GameObject>(particlePrefab);

        var main = particleGo.GetComponent<ParticleSystem>().main;
        Color setColor = col;
        var color = main.startColor.color;

        color.r = setColor.r / 255f;
        color.g = setColor.g / 255f;
        color.b = setColor.b / 255f;
        main.startColor = color;

        this.particleGo.transform.position = trans.position;
    }
}
