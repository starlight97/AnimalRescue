using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class BasicWeapon : PlayerWeapon
{
    private List<GameObject> projectileList = new List<GameObject>();
    public GameObject doughnutProjectilePrefab;

    private Coroutine createRoutine;
    private DoughnutProjectile doughnut;

    private float size = 1.5f;
    private Vector3 dir;

    private Player player;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        this.player = this.playerTrans.gameObject.GetComponent<Player>();
        this.playerTrans = GameObject.Find("Player").transform.Find("model").transform;
        this.Create();
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
            var projectileGo = Instantiate<GameObject>(doughnutProjectilePrefab);

            projectileGo.transform.position = this.transform.position;
            doughnut = projectileGo.GetComponent<DoughnutProjectile>();
            this.transform.rotation = this.playerTrans.transform.rotation;
            this.dir = transform.forward;

            if (player.visibleEnemyList.Count != 0 && player.visibleEnemyList[0] != null)
            {
                var enemyPos = player.visibleEnemyList[0].transform.position;
                this.dir = enemyPos - this.playerTrans.position;
            }

            doughnut.Init(this.current_damage, this.current_attack_speed, this.dir.normalized);
            doughnut.transform.localScale = new Vector3(size, size, size);
            projectileList.Add(projectileGo);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Upgrade()
    {
        if (size <= 5)
        {
            size += 0.5f;
        }
        else
        {
            base.Upgrade();
        }
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }
}