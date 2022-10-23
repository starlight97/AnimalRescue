using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singing : PlayerWeapon
{
    private GameObject notesGo;
    public GameObject projectilePrefab;
    private Vector3 dir;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
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
            yield return new WaitForSeconds(5f);
            notesGo.gameObject.SetActive(true);
            var projectileGo = Instantiate<GameObject>(projectilePrefab);
            var singingProjectile = projectileGo.GetComponent<SingingProjectile>();
            var randPos = Random.Range(-1, 2);

            dir = new Vector3(randPos, 0, randPos);
            singingProjectile.Init(weaponData.damage, 3, dir);

            yield return new WaitForSeconds(5f);
            notesGo.gameObject.SetActive(false);
        }

    }

    public override void Upgrade()
    {
        base.Upgrade();
    }
}
