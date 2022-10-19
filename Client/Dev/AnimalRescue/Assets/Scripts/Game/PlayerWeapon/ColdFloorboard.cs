using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColdFloorboard : PlayerWeapon
{
    private List<Enemy> enemyList;
    private ParticleSystem particleSystem;

    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        this.enemyList = new List<Enemy>();
        this.particleSystem = this.GetComponent<ParticleSystem>();
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
        }
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
                break;
            case 8:
                break;
            case 9:
                break;
            case 10:                
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
