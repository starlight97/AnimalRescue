using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : PlayerWeapon
{
    public GameObject projectilePrefab;
    public List<ShootingStarProjectile> projectileList;

    public override void Init(WeaponData weaponData, Transform trans)
    {
        base.Init(weaponData, trans);
        Create(trans);
    }

    public void Create(Transform trans)
    {
        StartCoroutine(CreateRoutine(trans));
    }

    private IEnumerator CreateRoutine(Transform trans)
    {
        while (true)
        {
            var projectileGo = Instantiate<GameObject>(projectilePrefab);
            float randX = trans.position.x + Random.Range(-5, 6);
            float randZ = trans.position.z + Random.Range(-5, 6);
            projectileGo.transform.position = new Vector3(randX, 10, randZ);
            //projectileGo.transform.position = new Vector3(trans.position.x, 10, trans.position.z);
            
            var projectile = projectileGo.GetComponent<ShootingStarProjectile>();
            projectile.Init(100, current_attack_speed, Vector3.down);
            projectile.CreateParticle(projectileGo.transform);
            projectileList.Add(projectile); 
            yield return new WaitForSeconds(3f);
            projectileList.Remove(projectile);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();

        //projectile_current_count++;

        //Transform trans = GameObject.Find("Player").GetComponent<Transform>();

        //var projectileGo = Instantiate<GameObject>(projectilePrefab);
        //float randX = trans.position.x + Random.Range(-5, 6);
        //float randZ = trans.position.z + Random.Range(-5, 6);
        //projectileGo.transform.position = new Vector3(randX, 10, randZ);
        //var projectile = projectileGo.GetComponent<ShootingStarProjectile>();
        //projectile.Init(current_damage);
        //projectileList.Add(projectile);
    }
}
