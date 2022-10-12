using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStarProjectile : PlayerProjectile
{
    private Vector3 dir;
    private GameObject star;
    private Coroutine starMoveRoutine;

    public override void Init(int damage, float moveSpeed)
    {
        base.Init(damage, moveSpeed, dir);
        this.star = GameObject.Find("Star").GetComponent<GameObject>();
        MoveStar();
    }

    public override void Attack(Collider collider)
    {
        base.Attack(collider);
        if (collider.CompareTag("Enemy"))
            Destroy(this.gameObject);
    }

    public void MoveStar()
    {
        if (starMoveRoutine != null)
        {
            StopCoroutine(this.starMoveRoutine);
        }
        this.starMoveRoutine = StartCoroutine(MoveStarRoutine());
    }

    private IEnumerator MoveStarRoutine()
    {
        while (true)
        {
            this.dir = Vector3.down - this.star.transform.position;
            this.star.transform.Translate(dir * Time.deltaTime);
            this.star.transform.rotation = this.gameObject.transform.localRotation;
            if (this.star.transform.position.z == 5)
            {
                Destroy(this.gameObject);
            }
            yield return new WaitForSeconds(1f);
        }
    }
}
