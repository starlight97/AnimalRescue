using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRat : Enemy
{
    public GameObject bossRatDoughnutPrefab;
    public float radius;
    

    protected override IEnumerator AttackRoutine()
    {
        RadiusAttack();
        yield return new WaitForSeconds(this.attackspeed);
        this.attackRoutine = null;
    }

    private void RadiusAttack()
    {
        float degree = this.transform.rotation.y;
        var rot = this.transform.rotation;
        rot = Quaternion.Euler(0, rot.eulerAngles.y - 30, 0);
        //var radian = this.degree * Mathf.Deg2Rad;
        for (int i = 0; i < 3; i++)
        {
            var radian = degree * Mathf.PI / 180;
            var x = Mathf.Cos(radian) * radius;
            var z = Mathf.Sin(radian) * radius;
            var pos = new Vector3(x, 0, z);
            //this.endPoint.transform.position = pos;

            var bossRatDoughnutGo = Instantiate<GameObject>(this.bossRatDoughnutPrefab);

            // 타겟위치와 방향을 구한다 
            var dir = pos - bossRatDoughnutGo.transform.position;

            // 각도(0, 0, 0)에서 dir(방향)만큼 회전한 회전값 
            Vector3 rotation = Quaternion.Euler(0, 0, 0) * dir;
            // 회전축 , 회전값 
            //var rot = Quaternion.LookRotation(Vector3.forward, rotation);
            //bossRatDoughnutGo.transform.rotation = rot;
            //bossRatDoughnutGo.transform.rotation = this.transform.rotation;            
            rot = Quaternion.Euler(0, rot.eulerAngles.y + (30*i), 0);
            bossRatDoughnutGo.transform.rotation = rot;
            bossRatDoughnutGo.transform.position = pos + this.transform.position;

            var bossRatDoughnu = bossRatDoughnutGo.GetComponent<BossRatDoughnut>();
            bossRatDoughnu.Init(dir, this.damage);

            // 각도 30씩 올리기 
            //degree += 45;
        }

    }

    private void Test()
    {
        for (int i = 0; i < 3; i++)
        {
            var rot = this.transform.rotation;
            var projectileGo = Instantiate<GameObject>(bossRatDoughnutPrefab);
            projectileGo.transform.position = this.transform.position;
            var doughnut = projectileGo.GetComponent<BossRatDoughnut>();
            this.transform.rotation = this.transform.rotation;
            doughnut.Init(this.transform.forward, this.damage);
        }
      
    }

}
