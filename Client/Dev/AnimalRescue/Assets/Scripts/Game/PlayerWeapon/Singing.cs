using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singing : PlayerWeapon
{
    private GameObject notesGo;
    public GameObject projectilePrefab;
    private Vector3 dir;
    
    private float attackSpeed;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        attackSpeed = this.current_attack_speed;
        // 투사체 날아갈 때마다 머리 위에 음표 띄움
        var player = GameObject.Find("Player").gameObject;
        notesGo = player.transform.Find("Notes").gameObject;
        notesGo.transform.position = new Vector3(0, 2.7f, 0);
        notesGo.gameObject.SetActive(false);
        Create();
    }

    private void Create()
    {
        StartCoroutine(CreateRoutine());
    }

    private IEnumerator CreateRoutine()
    {
        while(true)
        {
            notesGo.gameObject.SetActive(true);
            var projectileGo = Instantiate<GameObject>(projectilePrefab);
            var singingProjectile = projectileGo.GetComponent<SingingProjectile>();
            singingProjectile.transform.position = playerTrans.position;

            // 반경 1을 갖는 구의 랜덤 위치로 이동
            dir = Random.insideUnitSphere.normalized;
            dir.y = 0;

            singingProjectile.Init(current_damage, attackSpeed, dir);

            yield return new WaitForSeconds(3f);

            notesGo.gameObject.SetActive(false);

            yield return new WaitForSeconds(3f);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();
        switch (level)
        {
            case 3:
                IncreaseAttackSpeed();
                break;
            case 6:
                IncreaseAttackSpeed();
                break;
            case 9:
                IncreaseAttackSpeed();
                break;
            default:
                break;
        }
    }

    private void IncreaseAttackSpeed()
    {
        this.attackSpeed += 1;
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }
}
