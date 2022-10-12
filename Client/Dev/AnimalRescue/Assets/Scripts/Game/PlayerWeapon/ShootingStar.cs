using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : PlayerWeapon
{
    private GameObject projectilePrefab;

    public override void Init(WeaponData weaponData)
    {
        base.Init(weaponData);
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
            projectileGo.transform.position = this.transform.position;
            yield return null;
        }
    }
}
