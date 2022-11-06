using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITitleHero : MonoBehaviour
{
    private Animator anim;

    public void Init()
    {
        this.anim = GetComponent<Animator>();
    }

    public void RunAnimation()
    {
        this.anim.SetTrigger("Jump");
    }
}
