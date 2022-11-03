using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class JeongTest2Main : MonoBehaviour
{
    public Text textTime;
    private float delta = 0;
    void Start()
    {
        Timer timer = new Timer();
        timer.Start();
        
        timer.Interval = 1000;

        timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);  //주기마다 실행되는 이벤트 등록
        StartCoroutine(this.TimerRoutine());
    }

    delegate void TimerEventFiredDelegate();
    
    void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        delta += 1;
    }
    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            textTime.text = delta.ToString();
            yield return null;
            if (delta >= 1000f)
                break;
        }
    }
}
