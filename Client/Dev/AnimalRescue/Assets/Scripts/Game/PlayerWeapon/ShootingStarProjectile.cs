using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarProjectile : PlayerProjectile
{
    public GameObject star;
    [SerializeField]
    private float speed;
    private Vector3 dir;

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

    private void Start()
    {

        Debug.Log(dir);
    }

    public void StarMove()
    {
        StartCoroutine(StarMoveRoutine());
    }

    private IEnumerator StarMoveRoutine()
    {
        while (true)
        {
            var groundPos = new Vector3(0, 0, -5);
            this.dir = this.star.transform.localPosition - groundPos;
            this.star.transform.Translate(this.dir * Time.deltaTime * speed);
            if (this.star.transform.position.y <= 0)
            {
                Destroy(this.gameObject);
            }
            yield return null;
        }
    }
}
