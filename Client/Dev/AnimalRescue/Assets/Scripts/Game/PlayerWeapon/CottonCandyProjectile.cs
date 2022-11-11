using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CottonCandyProjectile : PlayerProjectile
{
    public AudioClip attackAudio;
    public override void Init(int damage)
    {
        base.Init(damage);
    }

    public override void Attack(Collider collider)
    {
        base.Attack(collider);
        if (collider.tag == "Enemy")
        {
            SoundManager.instance.PlaySound(attackAudio);
        }
    }

    protected override IEnumerator MoveRoutine()
    {
        while(true)
        {
            yield return null;
        }
    }




}
