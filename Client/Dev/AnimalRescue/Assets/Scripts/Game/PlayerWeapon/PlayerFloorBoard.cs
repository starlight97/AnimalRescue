using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorBoard : MonoBehaviour
{
    private int damage;
    private float attackDelay;

    private List<Enemy> enemyList;

    private void Start()
    {
        this.Init(10);
    }

    public void Init(int damage)
    {
        this.enemyList = new List<Enemy>();
        this.damage = damage;
        StartCoroutine(this.AttackRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Remove(other.GetComponent<Enemy>());
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            int enemyCount = enemyList.Count;
            for (int index = enemyCount-1; index >= 0; index--)
            {
                // enemy가 다른무기에 이미 죽었다면
                if (enemyList[index] == null)
                    continue;

                if (enemyList[index].currentHp > 0)
                    enemyList[index].Hit(damage);
                else
                    enemyList.RemoveAt(index);
            }

            yield return new WaitForSeconds(0.1f);
        }

    }
}
