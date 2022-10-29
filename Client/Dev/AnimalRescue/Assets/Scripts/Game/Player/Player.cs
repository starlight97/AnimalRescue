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

    public int Id { get; private set; }

    private GameObject heroGo;

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
        // 참조 없어지면 철거할 예정 ^__^!!
    }

    public void Init(int heroId)
    {
        this.Id = heroId;

        this.playerStats = GetComponent<PlayerStats>();
        this.playerMove = GetComponent<PlayerMove>();

        var heroData = DataManager.instance.GetData<HeroData>(heroId);
        var info = InfoManager.instance.GetInfo();

        var heroDamage = (heroData.damage + info.dicHeroInfo[heroData.id].dicStats["damage"] * heroData.increase_damage);
        var heroMaxHp = (heroData.max_hp + info.dicHeroInfo[heroData.id].dicStats["maxHp"] * heroData.increase_maxhp);
        var heroMoveSpeed = (heroData.move_speed + info.dicHeroInfo[heroData.id].dicStats["moveSpeed"] * heroData.increase_movespeed);

        this.heroGo = Instantiate(Resources.Load<GameObject>(heroData.prefab_path));
        this.heroGo.name = "model";
        this.heroGo.transform.parent = this.transform;

        this.hpGaugePoint = this.transform.Find("HpGaugePoint").GetComponent<Transform>();
        this.anim = this.GetComponentInChildren<Animator>();
        SetState(eStateType.Idle);
        // Hp
        playerLife.MaxHp = heroMaxHp;
        playerLife.Hp = playerLife.MaxHp;

        this.playerStats.Init(heroDamage, playerLife.MaxHp, heroMoveSpeed, 0);

        this.playerMove.Init();
        this.playerMove.Move(heroMoveSpeed);

        this.playerMove.onMove = () =>
        {
            SetState(eStateType.Run);
        };

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