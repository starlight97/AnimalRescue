using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarProjectile : PlayerProjectile
{
    public GameObject star;
    [SerializeField]
    private float speed;

    public override void Init(int damage)
    {
        base.Init(damage);
        StarMove();
    }

    public override void Attack(Collider collider)
    {
        base.Attack(collider);
        if (collider.CompareTag("Enemy"))
            Destroy(this.gameObject);
    }

    public void StarMove()
    {
        StartCoroutine(StarMoveRoutine());
    }

    private IEnumerator StarMoveRoutine()
    {
        while (true)
        {
            var groundPos = new Vector3(this.star.transform.position.x, this.star.transform.position.y, -5);
            var dir = groundPos - this.star.transform.localPosition;
            this.star.transform.Translate(dir * Time.deltaTime * speed);
            if (this.star.transform.position.y <= 0)
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
}
