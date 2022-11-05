using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossElementalGolemStone : MonoBehaviour
{
    public float speed;
    private int damage;
    private GameObject shadowGo;
    public void Init(int damage, GameObject shadowGo)
    {
        this.damage = damage;
        this.shadowGo = shadowGo;
        this.StartCoroutine(this.MoveRoutine());
    }


    private IEnumerator MoveRoutine()
    {
        while (true)
        {
            this.transform.Translate(Vector3.down * this.speed * Time.deltaTime);

            if (this.transform.position.y <= 0.1f)
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
            Destroy(this.gameObject);
        }
    }

    private void OnDestroy()
    {
        Destroy(shadowGo);
    }
}
