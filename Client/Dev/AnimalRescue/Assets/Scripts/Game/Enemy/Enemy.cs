using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public enum eState
    {
        Run, Attack, Hit, Die
    }

    protected int maxHp;
    public int currentHp;
    protected int damage;
    public int experience;
    protected float movespeed;
    protected float attackspeed;
    private GameObject playerGo;
    private Animator anim;

    protected Coroutine hitRoutine;
    protected Coroutine attackRoutine;

    public UnityAction<Enemy> onDie;
    public float attackRange;

    public void Init(int maxHp, int damage, int experience, float movespeed, float attackspeed)
    {
        this.maxHp = maxHp;
        this.currentHp = this.maxHp;
        this.damage = damage;
        this.experience = experience;
        this.movespeed = movespeed;
        this.attackspeed = attackspeed;

        this.playerGo = GameObject.Find("Player").gameObject;
        this.anim = this.GetComponent<Animator>();
        this.Move();
    }

    private void Move()
    {
        StartCoroutine(this.MoveRoutine());
    }

    private IEnumerator MoveRoutine()
    {
        while(true)
        {
            var distance = Vector3.Distance(this.transform.position, this.playerGo.transform.position);
            if (distance >= attackRange)
            {
                this.transform.LookAt(this.playerGo.transform.position);

                transform.Translate(Vector3.forward * Time.deltaTime * this.movespeed);
            }
            else
            {
                this.Attack();
            }
            yield return null;
        }
    }

    // 공격 당할때 호출
    public void Hit(int damage)
    {
        this.currentHp -= damage;

        // 피격 루틴 다 실행 하고 체력이 0 이하라면 죽는다.
        if (this.currentHp <= 0)
        {
            this.Die();
        }
        else
        {
            if (hitRoutine != null)
                StopCoroutine(hitRoutine);

            StartCoroutine(this.HitRoutine(damage));
        }
    }
    private IEnumerator HitRoutine(int damage)
    {
        SetState(eState.Hit);
        var length = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);
        hitRoutine = null;
        
        SetState(eState.Run);

    }

    // 죽을때 호출
    private void Die()
    {
        StartCoroutine(this.DieRoutine());
    }

    private IEnumerator DieRoutine()
    {
        SetState(eState.Die);
        var length = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);
        this.onDie(this);
    }

    // 공격 할 때 호출
    protected virtual void Attack()
    {
        if (attackRoutine == null)
            attackRoutine = StartCoroutine(this.AttackRoutine());
    }

    protected virtual IEnumerator AttackRoutine()
    {
        SetState(eState.Attack);
        var length = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);

        SetState(eState.Run);
        // 플레이어 공격
        var player = playerGo.GetComponent<Player>();
        player.Hit(this.damage);

        yield return this.attackspeed;
        this.attackRoutine = null;
    }

    private void SetState(eState state)
    {
        //anim.SetInteger("State", (int)state);
        anim.ResetTrigger(state.ToString());
        anim.SetTrigger(state.ToString());
    }
}
