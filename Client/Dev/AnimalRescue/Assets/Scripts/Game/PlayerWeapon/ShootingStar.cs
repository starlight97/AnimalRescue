using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : PlayerWeapon
{
    public GameObject projectilePrefab;
    public List<ShootingStarProjectile> projectileList;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
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
            float randX = playerTrans.position.x + Random.Range(-5, 6);
            float randZ = playerTrans.position.z + Random.Range(-5, 6);
            projectileGo.transform.position = new Vector3(randX, 10, randZ);

            var projectile = projectileGo.GetComponent<ShootingStarProjectile>();
            projectile.Init(current_damage, current_attack_speed, Vector3.down);            
            projectile.CreateParticle(projectileGo.transform);

            projectileList.Add(projectile);

            yield return new WaitForSeconds(3f);
            projectileList.Remove(projectile);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();

        switch (level)
        {
            case 1:
                CreateProjectile();
                Debug.Log("d");
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            case 6:
                break;

            default:
                break;
        }
    }
    //if (projectile_current_count <= weaponData.projectile_max_state)
    //{
    //    projectile_current_count++;
    //    var projectileGo = Instantiate<GameObject>(projectilePrefab);
    //    float randX = playerTrans.position.x + Random.Range(-5, 6);
    //    float randZ = playerTrans.position.z + Random.Range(-5, 6);
    //    projectileGo.transform.position = new Vector3(randX, 10, randZ);
    //    var projectile = projectileGo.GetComponent<ShootingStarProjectile>();
    //    projectile.Init(current_damage, current_attack_speed, Vector3.down);
    //    projectile.CreateParticle(projectileGo.transform);
    //}

    private void CreateProjectile()
    {
        var projectileGo = Instantiate<GameObject>(projectilePrefab);
        float randX = playerTrans.position.x + Random.Range(-5, 6);
        float randZ = playerTrans.position.z + Random.Range(-5, 6);
        projectileGo.transform.position = new Vector3(randX, 10, randZ);
        var projectile = projectileGo.GetComponent<ShootingStarProjectile>();

        projectile.Init(current_damage, current_attack_speed, Vector3.down);
        projectile.CreateParticle(projectileGo.transform);
    }
}
