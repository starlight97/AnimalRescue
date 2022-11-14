using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEditor;  // OnDrawGizmos

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
    private bool isDIe = false;

    public void Init(int heroId)
    {
        this.Id = heroId;

        var heroData = DataManager.instance.GetData<HeroData>(this.Id);
        var info = InfoManager.instance.GetInfo();

        this.heroGo = Instantiate(Resources.Load<GameObject>(heroData.prefab_path));
        this.heroGo.name = "model";
        this.heroGo.transform.parent = this.transform;


        this.hpGaugePoint = this.transform.Find("HpGaugePoint").GetComponent<Transform>();
        this.anim = this.GetComponentInChildren<Animator>();
        SetState(eStateType.Idle);

        this.playerStats = GetComponent<PlayerStats>();
        this.playerMove = GetComponent<PlayerMove>();

        this.playerMove.onMove = () =>
        {
            if (hitRoutine != null)
            {
                return;
            }
            SetState(eStateType.Run);
        };
        this.playerMove.onMoveComplete = () => 
        {
            if (this.hitRoutine == null)
                SetState(eStateType.Idle);
        };

        this.playerStats.Init(this.Id, 1);

        this.playerMove.moveSpeed = this.playerStats.moveSpeed;

        this.playerMove.Init();
        playerLife.MaxHp = playerStats.maxHp;
        playerLife.Hp = playerLife.MaxHp;

        FindEnemys();

        this.playerStats.onLevelUp = (amount) =>
        {
            this.onLevelUp(amount);
        };

        this.onUpdateMove(this.hpGaugePoint.position);
    }

    public void Recovery(float hp, float maxHp, float per)
    {
        playerLife.Hp *= per;
        if (playerLife.Hp >= playerLife.MaxHp)
            playerLife.Hp = playerLife.MaxHp;
        onUpdateHp(hp, maxHp);
    }

    public void Hit(int damage)
    {
        this.playerLife.Hp -= damage;

        if (this.playerLife.Hp <= 0)
        {
            this.Die();
        }
        else
        {
            if (this.hitRoutine != null)
                StopCoroutine(hitRoutine);
            this.hitRoutine = StartCoroutine(HitRoutine());
            onUpdateHp(this.playerLife.Hp, this.playerLife.MaxHp);
        }
    }

    private IEnumerator HitRoutine()
    {
        SetState(eStateType.Hit);
        var length = this.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);
        this.hitRoutine = null;
    } 

    private void Die()
    {
        if(isDIe == false)
        {
            StartCoroutine(DieRoutine());
        }
    }

    private IEnumerator DieRoutine()
    {
        isDIe = true;
        SetState(eStateType.Die);
        var length = this.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        yield return new WaitForSeconds(length);

        this.onDie();
    }

    private void SetState(eStateType state)
    {
        this.anim.SetInteger("State", (int)state);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            Enemy enemy = collision.collider.GetComponent<Enemy>();
            this.Hit(enemy.damage);
        }
    }



    #region 자동 에임 부채꼴 범위
    // 시야 영역의 반지름과 시야 각도
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;

    public LayerMask enemyMask;

    // Target mask에 ray hit된 transform을 보관하는 리스트
    public List<Transform> visibleEnemyList = new List<Transform>();

    public void FindEnemys()
    {
        StartCoroutine(FindEnemysWithDelay(0.1f));
    }

    IEnumerator FindEnemysWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            FindVisibleEnemys();
        }
    }

    void FindVisibleEnemys()
    {
        visibleEnemyList.Clear();
        // viewRadius를 반지름으로 한 원 영역 내 targetMask 레이어인 콜라이더를 모두 가져옴
        Collider[] EnemysInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, enemyMask);

        for (int i = 0; i < EnemysInViewRadius.Length; i++)
        {
            Transform target = EnemysInViewRadius[i].transform;
            Vector3 dirToTarget = (target.position - transform.position).normalized;

            // 플레이어와 forward와 target이 이루는 각이 설정한 각도 내라면
            if (Vector3.Angle(this.heroGo.transform.forward, dirToTarget) < viewAngle / 2)
            {
                float dstToTarget = Vector3.Distance(transform.position, target.transform.position);
                visibleEnemyList.Add(target);
            }
        }
    }

    // y축 오일러 각을 3차원 방향 벡터로 변환한다.
    // 참조 사이트: https://nicotina04.tistory.com/197
    public Vector3 DirFromAngle(float angleDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleDegrees += transform.eulerAngles.y;
        }

        return new Vector3(Mathf.Cos((-angleDegrees + 90) * Mathf.Deg2Rad), 0, Mathf.Sin((-angleDegrees + 90) * Mathf.Deg2Rad));
    }
    #endregion
}