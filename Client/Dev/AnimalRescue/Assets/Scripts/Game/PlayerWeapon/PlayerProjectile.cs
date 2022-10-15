using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public int damage;
    private float moveSpeed;

    // 무기자체가 투사체를 움직이는 경우가 있더라.
    virtual public void Init(int damage)
    {
        this.damage = damage;
    }

    virtual public void Init(int damage, float moveSpeed)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;
    }
    virtual public void Init(int damage, float moveSpeed, Vector3 dir)
    {
        this.damage = damage;
        this.moveSpeed = moveSpeed;

        StartCoroutine(this.MoveRoutine(dir));
    }

    private void OnTriggerEnter(Collider collider)
    {
        Attack(collider);
    }

    // Enemy와 충돌시 해당 Enemy를 공격한다!
    virtual public void Attack(Collider collider)
    {
        if (collider.tag == "Enemy")
        {
            Debug.Log(this.damage);
            Enemy enemy = collider.gameObject.GetComponent<Enemy>();
            enemy.Hit(this.damage);
        }
    }

    // 타겟 방향으로 1직선으로 날아가는 이동
    virtual protected IEnumerator MoveRoutine(Vector3 dir)
    {
        float time = 0;
        while(true)
        {
            // 임시로 10초동안 날라가도 안부딪히면 삭제
            time += Time.deltaTime;
            if(time > 3f)
            {
                Destroy(this.gameObject);
            }
            this.gameObject.transform.Translate(dir * Time.deltaTime * this.moveSpeed);
            yield return null;
        }
    }

    // 그냥 이동
    virtual protected IEnumerator MoveRoutine()
    {
        while (true)
        {
            yield return null;
        }
    }

}
