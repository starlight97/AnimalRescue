using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayTimeManager : MonoBehaviour
{
    private float delta;
    public UnityAction<string> onPassesTime;

    public void Init()
    {
        RunTime();
    }

    public void RunTime()
    {
        StartCoroutine(this.RunTimeRoutine());
    }

    private IEnumerator RunTimeRoutine()
    {
        while (true)
        {
            delta += Time.deltaTime;
            TimeSpan t = TimeSpan.FromSeconds(delta);

            string time = string.Format("{0:D2}:{1:D2}",
                            t.Minutes,
                            t.Seconds);
            onPassesTime(time);
            yield return null;
        }
    }
}
