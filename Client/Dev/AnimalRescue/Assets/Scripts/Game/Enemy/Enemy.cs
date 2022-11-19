using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public int id;
    public enum eState
    {
        Run, Attack, Hit, Die
    }

    protected int maxHp;
    public int currentHp;
    public int damage;
    public int experience;
    protected float attackSpeed;
    protected int level;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attackRange;

    protected GameObject playerGo;
    private Animator anim;

    protected Coroutine hitRoutine;
    protected Coroutine attackRoutine;

    public UnityAction<Enemy> onDie;

    public virtual void Init(int level, int maxHp, int damage, int experience, float movespeed, float attackspeed, float attackRange)
    {
        this.level = level;
        this.maxHp = this.level * maxHp;        
        this.currentHp = this.maxHp;
        //this.damage = this.level * damage;
        this.damage = damage + (this.level * damage / 5);
        this.experience = experience;
        this.moveSpeed = movespeed;
        this.attackSpeed = attackspeed;
        this.attackRange = attackRange;        

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

                transform.Translate(Vector3.forward * Time.deltaTime * this.moveSpeed);
            }
            else
            {
                this.transform.LookAt(this.playerGo.transform.position);
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

    protected virtual void DifficultySetting()
    {

    }

    protected virtual IEnumerator AttackRoutine()
    {
        SetState(eState.Attack);
        var length = anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);

        var dis = Vector3.Distance(playerGo.transform.position, this.gameObject.transform.position);
        if (dis <= this.attackRange)
        {
            var player = playerGo.GetComponent<Player>();
            player.Hit(this.damage);
        }
        else
        {
        }
        SetState(eState.Run);
        // 플레이어 공격


        yield return this.attackSpeed;
        this.attackRoutine = null;
    }

    public void StopAttack()
    {
        attackRoutine = null;
    }

    private void SetState(eState state)
    {
        //anim.SetInteger("State", (int)state);
        anim.ResetTrigger(state.ToString());
        anim.SetTrigger(state.ToString());
    }
}
