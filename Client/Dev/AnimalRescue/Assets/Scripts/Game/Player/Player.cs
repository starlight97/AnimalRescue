using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    private enum eStateType
    {
        None = -1,
        Idle, Run, Die, Hit
    }

    private PlayerMove playerMove;
    private PlayerStats playerStats;
    private CottonCandy weapon01;
    private BasicWeapon basicWeapon;

    [SerializeField]
    private int hp;
    [SerializeField]
    private int maxHp = 10;

    private Coroutine hitRoutine; 
    public UnityAction onDie;
    private Transform modelTrans;
    private Animator anim;

    public Transform hpGaugePoint;
    public UnityAction<Vector3> onUpdateMove;
    public UnityAction<int> onLevelUp;
    public UnityAction<int, int> onHit;

    public void Init()
    {
        this.modelTrans = transform.Find("model").GetComponent<Transform>();
        this.anim = this.GetComponentInChildren<Animator>();

        SetState(eStateType.Idle);

        this.hp = this.maxHp; 
        this.playerMove = GetComponent<PlayerMove>();
        this.playerMove.Move();


        this.playerMove.onMove = () =>
        {
            SetState(eStateType.Run);
        };


        this.playerStats = GetComponent<PlayerStats>();
        this.playerStats.Init(0, 0, 0);

        this.playerStats.onLevelUp = (amount) => 
        {
            this.onLevelUp(amount);
        };


        this.onUpdateMove(this.hpGaugePoint.position);

        

        //this.basicWeapon = GameObject.Find("BasicWeapon").GetComponent<BasicWeapon>();
        //DataManager.instance.onDataLoadFinished.AddListener(() =>
        //{
        //    var data = DataManager.instance.GetData<WeaponData>(2000);
        //    basicWeapon.Init(data, this.modelTrans);
        //});
    }

    private void Update()
    {
        //// hp gauge test - 스페이스 누르면 피 깎임
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Hit(1);
        //}
    }

    public void Hit(int damage)
    {
        this.hp -= damage;

        if (this.hitRoutine == null)
            this.hitRoutine = StartCoroutine(HitRoutine());

        this.onHit(this.hp, this.maxHp);

        if (this.hp <= 0)
        {
            this.Die();
            StopAllCoroutines();
        }
    }

    private IEnumerator HitRoutine()
    {
        SetState(eStateType.Hit);
        var length = this.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);

        SetState(eStateType.Idle);
        this.hitRoutine = null;
    }

    private void Die()
    {
        StartCoroutine(DieRoutine());
    }  

    private IEnumerator DieRoutine()
    {
        SetState(eStateType.Die);
        var length = this.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);
        this.onDie();
    }

    private void SetState(eStateType state)
    {
        this.anim.SetInteger("State", (int)state);
    }
}
