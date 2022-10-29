using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAncientWarriorDoughnut : MonoBehaviour
{
    public float speed;
    public int damage;

    public void Init(int damage)
    {
        this.damage = damage;
        this.StartCoroutine(this.MoveRoutine());
    }


    private IEnumerator MoveRoutine()
    {
        float delta = 0;
        while (true)
        {
            delta += Time.deltaTime;
            this.transform.Translate(Vector3.forward * this.speed * Time.deltaTime);

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
