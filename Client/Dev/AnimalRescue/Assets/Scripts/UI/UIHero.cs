using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHero : MonoBehaviour
{
    public enum eState
    {
        Idle = 0,
        PowerUp01,
        PowerUp02
    }
    private Animator anim;
    private Coroutine setAnimRoutine;
    public void Init()
    {
        this.anim = GetComponent<Animator>();
    }

    public void SetAnim(eState state)
    {
        //this.anim.SetTrigger(state.ToString());
        if (setAnimRoutine != null)
        {
            StopCoroutine(setAnimRoutine);
        }
        this.setAnimRoutine = StartCoroutine(this.SetAnimRoutine(state));
    }

    private IEnumerator SetAnimRoutine(eState state)
    {
        this.anim.Play(state.ToString(), -1, 0f);
        yield return null;
        if(this.anim.GetCurrentAnimatorClipInfo(0).Length>0)
        {
            var length = this.anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }
            
        //yield return new WaitForSeconds(length);
        this.anim.SetTrigger(eState.Idle.ToString());
        yield return null;
        this.setAnimRoutine = null;
    }


}
