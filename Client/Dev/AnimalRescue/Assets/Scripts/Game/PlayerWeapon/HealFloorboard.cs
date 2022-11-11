using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealFloorboard : PlayerWeapon
{
    private float per = 1.01f;
    private ParticleSystem ps;
    public override void Init(WeaponData weaponData, Transform playerTrans)
    {
        base.Init(weaponData, playerTrans);
        current_damage = 0;
        this.ps = this.transform.Find("Smoke").gameObject.GetComponent<ParticleSystem>();
        Heal();
    }

    public void Heal()
    {
        StartCoroutine(HealRoutine());
    }

    private IEnumerator HealRoutine()
    {
        while (true)
        {
            var player = playerTrans.GetComponent<Player>();
            player.Recovery(player.playerLife.Hp, player.playerLife.MaxHp, per);
            yield return new WaitForSeconds(1f);
        }
    }


    // 힐량 0.1퍼씩 증가
    public override void Upgrade()
    {
        base.Upgrade();
        this.per += 0.01f;

        switch (level)
        {
            case 2:
                ChangeAlpha(50f);
                break;
            case 3:
                ChangeAlpha(70f);
                break;
            case 4:
                ChangeAlpha(90f);
                break;
            case 5:
                ScaleUp(0.1f);
                ChangeColor(43, 255, 13);
                ChangeAlpha(100f);
                break;
            case 6:
                ChangeAlpha(110f);
                break;
            case 7:
                ChangeAlpha(120f);
                break;
            case 8:
                ChangeAlpha(130f);
                break;
            case 9:
                ChangeAlpha(140f);
                break;
            case 10:
                ScaleUp(0.2f);
                ChangeColor(13, 255, 159);
                ChangeAlpha(150f);
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

    private void ChangeColor(float r, float g, float b)
    {
        var main = ps.main;
        var color = main.startColor.color;
        color.r = r / 255f;
        color.g = g / 255f;
        color.b = b / 255f;
        main.startColor = color;
    }

    private void ChangeAlpha(float value)
    {
        var main = ps.main;
        var color = main.startColor.color;
        color.a = value / 255f;
        main.startColor = color;
    }

    private void LateUpdate()
    {
        this.FollowPlayer();
    }
}
