using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class JeongTest2Main : MonoBehaviour
{
    public Text textTime;
    public Button btnPause;
    private Timer timer;
    private float delta = 0;
    private float currentTime = 0;
    void Start()
    {        
        this.btnPause.onClick.AddListener(() =>
        {
            MyTimer();
            Time.timeScale = 0;
        });

        StartCoroutine(this.TimerRoutine());
    }

    private void MyTimer()
    {
        timer = new Timer();
        timer.Start();
        // 밀리세컨드 단위 1000 = 1초
        timer.Interval = 1000;

        timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);  //주기마다 실행되는 이벤트 등록
    }

    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            currentTime += Time.deltaTime;
            textTime.text = currentTime.ToString();
            yield return null;

            if (delta >= 5)
            {
                delta = 0;
                Time.timeScale = 1;
                timer.Stop();
                timer.Dispose();
            }
        }        
    }

    delegate void TimerEventFiredDelegate();
    void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        // 1초에 1씩증가
        delta += 1;
    }
}
