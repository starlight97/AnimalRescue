using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdFloorboard : PlayerWeapon
{
    private List<Enemy> enemyList;
    private Transform playerTrans;


    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        this.enemyList = new List<Enemy>();
        StartCoroutine(this.AttackRoutine());
        StartCoroutine(this.FollowPlayerRoutine());
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
            for (int index = enemyCount - 1; index >= 0; index--)
            {
                // enemy가 다른무기에 이미 죽었다면
                if (enemyList[index] == null)
                    continue;

                if (enemyList[index].currentHp > 0)
                    enemyList[index].Hit(this.current_damage);
                else
                    enemyList.RemoveAt(index);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
    private IEnumerator FollowPlayerRoutine()
    {
        while (true)
        {
            this.transform.position = playerTrans.position;
            yield return null;
        }
    }

}
