using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRatDoughnut : MonoBehaviour
{
    private Vector3 dir;
    public float speed;
    public int damage;

    public void Init(Vector3 dir, int damage)
    {
        this.dir = dir;
        //this.dir.y = this.transform.position.y;
        this.damage = damage;
        this.dir = Vector3.forward;

        this.StartCoroutine(this.MoveRoutine());
    }


    private IEnumerator MoveRoutine()
    {
        float delta = 0;
        while (true)
        {
            delta += Time.deltaTime;
            this.transform.Translate(dir * this.speed * Time.deltaTime);

            if (delta >= 5f)
                break;
            yield return null;
        }
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Hit(this.damage);
            //Destroy(this.gameObject);
        }
    }
}
