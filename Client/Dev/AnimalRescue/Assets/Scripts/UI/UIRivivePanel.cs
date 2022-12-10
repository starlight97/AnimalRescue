using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Timers;

public class UIRivivePanel : MonoBehaviour
{
    private GameObject panelGo;

    public Button noBtn;
    public Button adsBtn;
    public Text timeText;

    public UnityAction onClickNoBtn;
    public UnityAction onClickAdsBtn;
    public UnityAction onTimeOver;

    private int cnt = 10;
    private float delta = 0;
    private bool isStop = false;

    delegate void TimerEventFiredDelegate();

    public void Init()
    {
        this.panelGo = GameObject.Find("UIRivivePanel").transform.Find("Panel").gameObject;

        this.panelGo.gameObject.SetActive(false);

        this.noBtn.onClick.AddListener(() => 
        {
            Debug.Log("no");
            onClickNoBtn();
        });

        this.adsBtn.onClick.AddListener(() => 
        {
            Debug.Log("ads");
            this.isStop = true;
            onClickAdsBtn();
        });

    }
    
    public void ShowPanel()
    {
        this.panelGo.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        this.panelGo.gameObject.SetActive(false);
    }

    public void StopTimer()
    {
        isStop = true;
    }

    public void RiviveTimer()
    {
        StartCoroutine(this.TimerRoutine());
        if (isStop)
            StopAllCoroutines();
    }

    void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        delta += 1;
    }

    private IEnumerator TimerRoutine()
    {
        Timer timer = new Timer();
        timer.Start();

        timer.Interval = 1000;

        timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);  //주기마다 실행되는 이벤트 등록

        while (true)
        {
            timeText.text = (cnt - delta).ToString();
            yield return null;
            
            if (isStop)
            {
                timer.Stop();
                break;
            }
            if (delta > 10f)
            {
                onTimeOver();
                timer.Stop();
                break;
            }
        }
    }
}
