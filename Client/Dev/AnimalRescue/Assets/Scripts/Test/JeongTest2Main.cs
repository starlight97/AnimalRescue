using System.Collections;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

public class JeongTest2Main : MonoBehaviour
{
    public Text textTime;
    public Button btnPause;
    public Button btnResume;
    private Timer timer;
    private float delta = 0;
    void Start()
    {
        this.btnPause.onClick.AddListener(() =>
        {
            MyTimer();
            Time.timeScale = 0;
            //btnPause.gameObject.SetActive(false);
            //btnResume.gameObject.SetActive(true);
        });
        this.btnResume.onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            //Time.timeScale = 1;
            //btnPause.gameObject.SetActive(true);
            //btnResume.gameObject.SetActive(false);
        });

        //StartCoroutine(this.TimerRoutine());
    }
    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            //delta += Time.deltaTime;
            textTime.text = delta.ToString();
            yield return null;
            //if (delta >= 10f)
            //    break;

            if (delta >= 10)
            {
                Time.timeScale = 1;
                timer.Stop();
                timer.Dispose();
            }
        }
        //textTime.text = "10";
        
    }

    private void MyTimer()
    {
        timer = new Timer();
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
}
