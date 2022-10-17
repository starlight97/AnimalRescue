using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonCandy : PlayerWeapon
{
    private List<GameObject> projectileGoList = new List<GameObject>();
    public GameObject cottonCandyProjectilePrefab;

    public float circleR; //반지름
    public float deg; //각도


    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        this.Attack();

        var projectTileGo = Instantiate<GameObject>(cottonCandyProjectilePrefab);
        projectTileGo.transform.parent = this.gameObject.transform;
        var projectTile = projectTileGo.GetComponent<CottonCandyProjectile>();
        projectTile.Init(this.weaponData.damage);
        this.projectileGoList.Add(projectTileGo);
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
                //Debug.Log(projectileGoList.Count);
                var projectileGoListCount = projectileGoList.Count;
                for (int i = 0; i < projectileGoListCount; i++)
                {
                    var rad = Mathf.Deg2Rad * (deg + (i * (360 / projectileGoListCount)));
                    var x = circleR * Mathf.Sin(rad);
                    var z = circleR * Mathf.Cos(rad);
                    projectileGoList[i].transform.position = playerTrans.position + new Vector3(x, 1, z);
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
        if(projectile_current_count < this.weaponData.projectile_max_state)
        {
            projectile_current_count++;
            var projectTileGo = Instantiate<GameObject>(cottonCandyProjectilePrefab);
            projectTileGo.transform.parent = this.gameObject.transform;
            //var projectTile = projectTileGo.GetComponent<CottonCandyProjectile>();
            //projectTile.Init(this.weaponData.damage);
            this.projectileGoList.Add(projectTileGo);
        }

        foreach (var projectileGo in projectileGoList)
        {
            var projectile = projectileGo.GetComponent<CottonCandyProjectile>();
            projectile.Init(this.current_damage);
        }
        //Debug.Log("UpGrade");
        //Debug.Log("Level : " + this.level);

    }

}
