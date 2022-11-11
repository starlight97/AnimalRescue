using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdFloorboard : PlayerWeapon
{
    private List<Enemy> enemyList;
    private ParticleSystem particleSystem;
    private Coroutine attackSoundPlay;
    public AudioClip attackSoundAudio;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        this.enemyList = new List<Enemy>();
        this.particleSystem = this.GetComponent<ParticleSystem>();
        this.current_damage += playerDamage / 10;
        this.weaponData.damage += playerDamage / 10;
        this.ChangeAlpha(30f);

        StartCoroutine(this.AttackRoutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Add(other.GetComponent<Enemy>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            enemyList.Remove(other.GetComponent<Enemy>());
            if(enemyList.Count <= 0)
            {
                if (attackSoundPlay != null)
                {
                    StopCoroutine(attackSoundPlay);
                    attackSoundPlay = null;
                    SoundManager.instance.StopSound();
                }

            }
        }
    }

    private void AttackSoundPlay()
    {
        if (attackSoundPlay != null)
            return;

        attackSoundPlay = StartCoroutine(AttackSoundPlayImpl());
    }
    private IEnumerator AttackSoundPlayImpl()
    {
        SoundManager.instance.PlaySound(attackSoundAudio);
        yield return new WaitForSeconds(270f);
        attackSoundPlay = null;
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            int enemyCount = enemyList.Count;
            for (int index = enemyCount - 1; index >= 0; index--)
            {
                // enemy가 다른무기에 이미 죽었다면
                if (enemyList[index] == null)
                    continue;

                if (enemyList[index].currentHp > 0)
                    enemyList[index].Hit(this.current_damage);
                else
                    enemyList.RemoveAt(index);
            }
            if (enemyCount > 0)
                AttackSoundPlay();

            yield return new WaitForSeconds(this.weaponData.attack_speed);
        }
    }

    public override void Upgrade()
    {
        base.Upgrade();

        switch (level)
        {
            case 1:
                this.ChangeAlpha(30f);
                break;
            case 2:
                this.ChangeAlpha(50f);
                break;
            case 3:
                this.ScaleUp();
                break;
            case 4:
                this.ChangeAlpha(70f);
                break;
            case 5:
                this.ChangeAlpha(90f);
                break;
            case 6:
                this.ScaleUp();
                break;
            case 7:
                this.ChangeAlpha(110f);
                break;
            case 8:
                this.ChangeAlpha(130f);
                break;
            case 9:
                this.ScaleUp();
                break;
            case 10:
                this.ChangeAlpha(150f);
                break;
            case 11:
                this.ChangeAlpha(170f);
                break;
            case 12:
                this.ScaleUp();
                break;
            case 13:
                this.SetScale(3f);
                this.ChangeColor(0, 47, 255);
                this.ChangeAlpha(30f);
                break;
            case 14:
                this.ChangeAlpha(50f);
                break;
            case 15:
                this.ChangeAlpha(70f);
                break;
            case 16:
                this.ScaleUp();
                break;
            case 17:
                this.ChangeAlpha(90f);
                break;
            case 18:
                this.ChangeAlpha(110f);
                break;
            case 19:
                this.ScaleUp();
                break;
            case 20:
                this.ChangeAlpha(130f);
                break;
            case 21:
                this.ChangeAlpha(150f);
                break;
            case 22:
                this.ScaleUp();
                break;
            case 23:
                this.ChangeAlpha(170f);
                break;
            case 24:
                this.SetScale(3f);
                this.ChangeColor(0, 17, 255);
                this.ChangeAlpha(30f);
                break;
            case 25:
                this.ChangeAlpha(50f);
                break;
            case 26:
                this.ChangeAlpha(70f);
                break;
            case 27:
                this.ScaleUp();
                break;
            case 28:
                this.ChangeAlpha(90f);
                break;
            case 29:
                this.ChangeAlpha(110f);
                break;
            case 30:
                this.ScaleUp();
                break;
            case 31:
                this.ChangeAlpha(130f);
                break;
            case 32:
                this.ChangeAlpha(150f);
                break;
            case 33:
                this.ScaleUp();
                break;
            case 34:
                this.ChangeAlpha(170f);
                break;
            default:
                break;
        }
    }

    // value = 스케일 올릴 사이즈
    private void ScaleUp(float value = 1.0f)
    {
        var scale = this.transform.localScale;
        scale.x += value;
        scale.y += value;
        scale.z += value;
        this.transform.localScale = scale;
    }
    // value = 스케일 올릴 사이즈
    private void SetScale(float value)
    {
        Vector3 scale = new Vector3();
        scale.x = value;
        scale.y = value;
        scale.z = value;
        this.transform.localScale = scale;
    }

    // rgb = 색상값
    // 0 ~ 255
    private void ChangeColor(float r, float g, float b)
    {
        var main = particleSystem.main;
        var color = main.startColor.color;
        color.r = r / 255f;
        color.g = g / 255f;
        color.b = b / 255f;
        main.startColor = color;
    }

    // value = 투명도
    // 0 ~ 255
    private void ChangeAlpha(float value)
    {
        var main = particleSystem.main;
        var color = main.startColor.color;
        color.a = value / 255f;
        main.startColor = color;
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }

}
