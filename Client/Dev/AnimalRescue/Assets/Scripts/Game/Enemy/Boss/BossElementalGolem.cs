using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossElementalGolem : Enemy
{
    public GameObject stonePrefab;
    public float radius;
    public GameObject shadowPrefab;
    public UnityEvent<BossElementalGolemStone> onCreateStone = new UnityEvent<BossElementalGolemStone>();
    private int attackCount;
    private float stoneRandomRange;

    public override void Init(int level, int maxHp, int damage, int experience, float movespeed, float attackspeed, float attackRange)
    {
        base.Init(level, maxHp, damage, experience, movespeed, attackspeed, attackRange);

        this.DifficultySetting();
    }

    protected override IEnumerator AttackRoutine()
    {
        SpawnStone(playerGo.transform.position);
        for (int count = 1; count < attackCount; count++)
        {
            Debug.Log("asd");
            var randX = Random.Range(-stoneRandomRange, stoneRandomRange);
            var randZ = Random.Range(-stoneRandomRange, stoneRandomRange);

            var pos = playerGo.transform.position;
            pos.x += randX;
            pos.z += randZ;
            SpawnStone(pos);
        }
        
        yield return new WaitForSeconds(this.attackSpeed);
        this.attackRoutine = null;
    }

    private void SpawnStone(Vector3 tpos)
    {
        var pos = new Vector3(tpos.x, 20, tpos.z);
        var stoneGo = Instantiate<GameObject>(this.stonePrefab);
        stoneGo.transform.position = pos;
        var shadowGo = Instantiate(this.shadowPrefab);
        pos.y = 0.01f;
        shadowGo.transform.position = pos;
        shadowGo.GetComponent<Shadow>().Init(stoneGo);

        var bossStone = stoneGo.GetComponent<BossElementalGolemStone>();
        bossStone.Init(this.damage, shadowGo);
    }


    protected override void DifficultySetting()
    {
        base.DifficultySetting();

        if (this.level == 1)
        {
            attackCount = 1;
            stoneRandomRange = 3f;
        }
        else if (this.level == 2)
        {
            attackCount = 2;
            stoneRandomRange = 5f;
        }
        else if (this.level == 3)
        {
            attackCount = 6;
            stoneRandomRange = 7f;
        }
        else if (this.level >= 4)
        {
            attackCount = 10;
            stoneRandomRange = 10f;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.level++;
            DifficultySetting();
        }
    }
}
