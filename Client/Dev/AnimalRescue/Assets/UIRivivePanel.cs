using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Timers;

public class UIRivivePanel : MonoBehaviour
{
    public Button noBtn;
    public Button adsBtn;
    public Text timeText;

    public UnityAction onClickNoBtn;
    public UnityAction onClickAdsBtn;
    public UnityAction onTimeOver;

    private int cnt = 10;
    private float delta = 0;
    delegate void TimerEventFiredDelegate();

    public void Init()
    {
        this.gameObject.SetActive(false);

        this.noBtn.onClick.AddListener(() => 
        {
            Debug.Log("no");
            // 아니오 버튼 누르면 GameOverScene으로 이동해야 함
            onClickNoBtn();
        });

        this.adsBtn.onClick.AddListener(() => 
        {
            Debug.Log("ads");
            // 광고 버튼 누르면 광고 띄워주고 HP 절반 채워줘야 함
            onClickAdsBtn();
        });

        // 광고를 본 후 죽었을 때는 팝업창 뜨지 않고 바로 GameOverScene으로 이동해야 함

    }
    
    public void ShowPanel()
    {
        this.gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        this.gameObject.SetActive(false);
    }

    public void RiviveTimer()
    {
        Timer timer = new Timer();
        timer.Start();

        timer.Interval = 1000;

        timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);  //주기마다 실행되는 이벤트 등록
        StartCoroutine(this.TimerRoutine());
    }

    void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
        delta += 1;
    }

    private IEnumerator TimerRoutine()
    {
        while (true)
        {
            timeText.text = (cnt - delta).ToString();
            yield return null;
            if (delta > 10f)
            {
                onTimeOver();
                break;
            }
        }
    }

}
