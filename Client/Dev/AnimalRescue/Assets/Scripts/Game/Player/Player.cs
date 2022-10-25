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

    public PlayerLife playerLife = new PlayerLife();
    private PlayerMove playerMove;
    private PlayerStats playerStats;

    private Coroutine hitRoutine;
    public UnityAction onDie;
    private Animator anim;

    private Transform hpGaugePoint;
    public UnityAction<Vector3> onUpdateMove;
    public UnityAction<int> onLevelUp;
    public UnityAction<float, float> onUpdateHp;

    public void Init()
    {
        playerLife.MaxHp = 100;
        playerLife.Hp = playerLife.MaxHp;
        this.hpGaugePoint = transform.Find("HpGaugePoint").GetComponent<Transform>();

        this.anim = this.GetComponentInChildren<Animator>();

        SetState(eStateType.Idle);

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
    }

    public void Recovery(float hp, float maxHp, float amount)
    {
        playerLife.Hp += amount;
        if (playerLife.Hp >= playerLife.MaxHp)
            playerLife.Hp = playerLife.MaxHp;
        onUpdateHp(hp, maxHp);
    }

    public void Hit(int damage)
    {
        this.playerLife.Hp -= damage;

        if (this.hitRoutine == null)
            this.hitRoutine = StartCoroutine(HitRoutine());
        onUpdateHp(this.playerLife.Hp, this.playerLife.MaxHp);

        if (this.playerLife.Hp <= 0)
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