using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIGame : UIBase
{
    private UIHpGauge uiHpGauge;
    private UIWeaponLevelUp uiWeaponLevelUp;
    private UIRivivePanel uiRivivePanel;
    private UIGameStatus uiGameStatus;
    private UIRotateDots uiRotateDots;
    public UnityAction<int> onWeaponSelect;
    public UnityAction onGameOver;
    public UnityAction onClickAds;
    public Button btnGameExit;

    public override void Init()
    {
        base.Init();
        this.UIOptionInit();
        this.uiHpGauge = this.transform.Find("UIHpGauge").GetComponent<UIHpGauge>();
        this.uiWeaponLevelUp = this.transform.Find("UIWeaponLevelUp").GetComponent<UIWeaponLevelUp>();
        this.uiRivivePanel = this.transform.Find("UIRivivePanel").GetComponent<UIRivivePanel>();
        this.uiGameStatus = this.transform.Find("UIGameStatus").GetComponent<UIGameStatus>();
        this.uiRotateDots = this.transform.Find("UIRotateDots").GetComponent<UIRotateDots>();

        
        uiWeaponLevelUp.onWeaponSelect = (id) =>
        {
            this.onWeaponSelect(id);
            this.uiWeaponLevelUp.HideUI();
        };

        this.isShowPanelOption = (check) =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);

            if (check)
                Pause();
            else
                Resume();
        };

        // 아니오 누르거나 시간초가 다 되었을 때
        uiRivivePanel.onClickNoBtn = () => 
        {
            this.uiRivivePanel.HidePanel();
            this.uiRivivePanel.StopTimer();
            this.onGameOver();
        };
        uiRivivePanel.onTimeOver = () => 
        {
            this.uiRivivePanel.HidePanel();
            this.uiRivivePanel.StopTimer();
            this.onGameOver();
        };
        // 광고 버튼 눌렀을 때
        uiRivivePanel.onClickAdsBtn = () =>
        {
            this.uiRotateDots.Show();
            this.uiRivivePanel.StopTimer();
            this.onClickAds();
        };

        uiHpGauge.Init();
        uiWeaponLevelUp.Init();
        uiRivivePanel.Init();
        uiGameStatus.Init();
        uiRotateDots.Init();
    }

    public void FixedHpGaugePosition(Vector3 worldPos)
    {
        this.uiHpGauge.UpdatePosition(worldPos);
    }

    public void UpdateUIHpGauge(float hp, float maxHp)
    {
        this.uiHpGauge.UpdateUI(hp, maxHp);
    }

    public void ShowWeaponLevelUp()
    {
        this.uiWeaponLevelUp.ShowUI();
    }

    public void ShowRivivePanel(bool value)
    {
        if (value)
            this.uiRivivePanel.ShowPanel();
        else
            this.uiRivivePanel.HidePanel();
    }

    public void RunTimer()
    {
        this.uiRivivePanel.RiviveTimer();
    }

    public void SetProgress(int enemyCount, int goldCount)
    {
        this.uiGameStatus.SetProgressText(enemyCount, goldCount);
    }

    public void SetWave(int wave)
    {
        this.uiGameStatus.SetWaveText(wave);
    }

    public void SetPlayTime(string time)
    {
        this.uiGameStatus.SetPlayTimeText(time);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        Time.timeScale = 0f;
    }

    public void HiderotateDots()
    {
        this.uiRotateDots.Hide();
    }
}
