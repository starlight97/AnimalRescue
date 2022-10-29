using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossForestGuardianCube : MonoBehaviour
{
    private int damage;
    private float cubeMaxScale;
    private float diffusionRate;
    public UnityAction onAttackComplete;
    public void Init(int damage, float cubeMaxScale, float diffusionRate)
    {
        this.damage = damage;
        this.cubeMaxScale = cubeMaxScale;

        StartCoroutine(this.AttackRoutine());
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().Hit(this.damage);
        }
    }

    private IEnumerator AttackRoutine()
    {
        while (true)
        {
            yield return null;
            var scale = this.transform.localScale;
            scale.x += diffusionRate;
            scale.z += diffusionRate;
            this.transform.localScale = scale;

            if (scale.x >= cubeMaxScale)
                break;
        }
        this.onAttackComplete();
        Destroy(this.gameObject);
    }
}
