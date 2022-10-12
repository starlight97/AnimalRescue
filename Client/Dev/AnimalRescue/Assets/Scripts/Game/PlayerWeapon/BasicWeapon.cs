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
    private Transform modelTrans;
    private DoughnutProjectile doughnut;

    private float size = 1;

    public override void Init(WeaponData weaponData, Transform trans)
    {
        base.Init(weaponData, trans);
        modelTrans = trans;
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
            this.transform.rotation = this.modelTrans.transform.rotation;
            doughnut.Init(this.current_damage, 10, this.transform.forward);
            doughnut.transform.localScale = new Vector3(size, size, size);
            projectileList.Add(projectileGo);
            yield return new WaitForSeconds(1f);
        }
    }

    public override void Upgrade()
    {
        if (size <= 5)
        {
            size += 1;
        }
        else
        {
            base.Upgrade();
        }
    }
}