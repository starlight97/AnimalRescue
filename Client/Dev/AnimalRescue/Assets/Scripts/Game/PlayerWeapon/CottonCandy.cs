using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonCandy : PlayerWeapon
{
    // 플레이어 트랜스폼받아서 플레이어 주변 돌게 할거에요
    private Transform centerTrans;

    private List<GameObject> projectileGoList = new List<GameObject>();
    public GameObject cottonCandyProjectilePrefab;

    public float circleR; //반지름
    public float deg; //각도


    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        this.Attack();
    }

    public void Attack()
    {
        this.StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            // 원운동 속도
            deg += Time.deltaTime * this.weaponData.projectile_speed;
            if (deg < 360)
            {
                var projectileGoListCount = projectileGoList.Count;
                for (int i = 0; i < projectileGoListCount; i++)
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / projectileGoListCount)));
                    var x = circleR * Mathf.Sin(rad);
                    var z = circleR * Mathf.Cos(rad);
                    projectileGoList[i].transform.position = centerTrans.position + new Vector3(x, 1, z);
                    //weapons[i].transform.rotation = Quaternion.Euler(0, 0, (deg + (i * (360 / 4))) * -1);
                }
            }
            else
            {
                deg = 0;
            }
            yield return null;
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();

        if(projectile_current_count <= this.weaponData.projectile_max_state)
        {
            projectile_current_count++;
            var projectTileGo = Instantiate<GameObject>(cottonCandyProjectilePrefab);
            projectTileGo.transform.parent = this.gameObject.transform;
            var projectTile = projectTileGo.GetComponent<CottonCandyProjectile>();
            projectTile.Init(this.weaponData.damage);
            this.projectileGoList.Add(projectTileGo);
        }
        else
        {
            // 투사체가 최대치까지 늘어났다면 그때부터는 공격력을 증가 시켜줘요.
            foreach (var projectileGo in projectileGoList)
            {
                var projectile = projectileGo.GetComponent<PlayerProjectile>();
                projectile.Init(this.current_damage);
            }
        }
        //Debug.Log("UpGrade");
        //Debug.Log("Level : " + this.level);
        
    }

}
