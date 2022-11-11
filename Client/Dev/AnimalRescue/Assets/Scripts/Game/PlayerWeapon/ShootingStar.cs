using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStar : PlayerWeapon
{
    public AudioClip bulletSpawnAudio;
    public GameObject projectilePrefab;
    public List<ShootingStarProjectile> projectileList;
    private Coroutine createRoutine;
    private Color particleColor = new Color(195, 29, 25);

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        this.current_damage += playerDamage * 3;
        this.weaponData.damage += playerDamage * 3;
        Create();
    }

    public void Create()
    {
        if (createRoutine == null)
            createRoutine = StartCoroutine(CreateRoutine());
    }

    private IEnumerator CreateRoutine()
    {
        while (true)
        {
            SoundManager.instance.PlaySound(bulletSpawnAudio);
            var projectileGo = Instantiate<GameObject>(projectilePrefab);
            
            float randX = playerTrans.position.x + Random.Range(-5, 6);
            float randZ = playerTrans.position.z + Random.Range(-5, 6);
            projectileGo.transform.position = new Vector3(randX, 10, randZ);

            // 별똥별 떨어질 때 파티클 + 콜라이더 함께 생성하여 떨어지게 작성됨
            var projectile = projectileGo.GetComponent<ShootingStarProjectile>();
            projectile.Init(current_damage, current_attack_speed, Vector3.down);
            projectile.CreateParticle(projectileGo.transform, this.particleColor);

            projectileList.Add(projectile);

            yield return new WaitForSeconds(3f);
            projectileList.Remove(projectile);
            this.createRoutine = null;
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        
        switch (level)
        {
            case 2:
                Create();
                break;
            case 3:
                Create();
                SetColor(200, 124, 50);
                break;
            case 4:
                Create();
                break;
            case 5:
                Create();
                SetColor(246, 235, 117);
                break;
            case 6:
                Create();
                break;
            case 7:
                Create();
                SetColor(193, 193, 193);
                break;
            case 8:
                Create();
                break;
            case 9:
                Create();
                SetColor(47, 108, 100);
                break;
            case 10:
                Create();
                break;
            case 11:
                Create();
                SetColor(4, 54, 118);
                break;

            default:
                break;
        }
    }

    private void SetColor(int r, int g, int b)
    {
        this.particleColor = new Color(r, g, b);
    }
}
