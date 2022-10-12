using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : PlayerWeapon
{
    public GameObject projectilePrefab;

    public override void Init(WeaponData weaponData)
    {
        base.Init(weaponData);
        Create();
    }

    public void Create()
    {
        StartCoroutine(CreateRoutine());
    }

    private IEnumerator CreateRoutine()
    {
        while (true)
        {
            var projectileGo = Instantiate<GameObject>(projectilePrefab);
            int randX = Random.Range(-10, 10);
            int randZ = Random.Range(-5, 5);
            projectileGo.transform.position = new Vector3(randX, 10, randZ);
            projectileGo.transform.parent = this.transform;
            var projectile = projectileGo.GetComponent<ShootingStarProjectile>();
            projectile.Init(current_damage);
            
            yield return new WaitForSeconds(3f);
        }
    }
}
