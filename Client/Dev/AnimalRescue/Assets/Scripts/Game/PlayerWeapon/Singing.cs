using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singing : PlayerWeapon
{
    private GameObject notesGo;
    public GameObject projectilePrefab;
    private Vector3 dir;
    
    private float attackSpeed;
    public AudioClip attackAudio;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        attackSpeed = this.current_attack_speed;
        current_damage += playerDamage / 7;
        this.weaponData.damage += playerDamage / 7;
        // 투사체 날아갈 때마다 머리 위에 음표 띄움
        var player = GameObject.Find("Player").gameObject;
        notesGo = player.transform.Find("Notes").gameObject;
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

            // 반경 1을 갖는 구의 랜덤 위치에 생성
            dir = Random.insideUnitSphere.normalized;
            dir.y = 0;

            singingProjectile.Init(current_damage, attackSpeed, dir);
            SoundManager.instance.PlaySound(attackAudio);
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
            case 4:
                IncreaseAttackSpeed();
                break;
            case 8:
                IncreaseAttackSpeed();
                break;
            case 12:
                IncreaseAttackSpeed();
                break;
            default:
                break;
        }
    }

    private void IncreaseAttackSpeed()
    {
        this.attackSpeed -= 0.1f;
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }
}
