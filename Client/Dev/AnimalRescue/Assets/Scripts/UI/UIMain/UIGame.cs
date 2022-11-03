using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIGame : MonoBehaviour
{
    private UIHpGauge uiHpGauge;
    private UIWeaponLevelUp uiWeaponLevelUp;
    private UIRivivePanel uiRivivePanel;
    public UnityAction<int> onWeaponSelect;
    public UnityAction onGameOver;

    public void Init()
    {
        this.uiHpGauge = this.transform.Find("UIHpGauge").GetComponent<UIHpGauge>();
        this.uiWeaponLevelUp = this.transform.Find("UIWeaponLevelUp").GetComponent<UIWeaponLevelUp>();
        this.uiRivivePanel = this.transform.Find("UIRivivePanel").GetComponent<UIRivivePanel>();
        
        uiWeaponLevelUp.onWeaponSelect = (id) =>
        {
            this.onWeaponSelect(id);
            this.uiWeaponLevelUp.HideUI();
        };

        uiRivivePanel.onClickNoBtn = () => 
        {
            this.uiRivivePanel.HidePanel();
            this.onGameOver();
        };

        uiRivivePanel.onClickAdsBtn = () =>
        {
            this.uiRivivePanel.HidePanel();
            AdMobManager.instance.ShowAds();
        };

        uiRivivePanel.onTimeOver = () => 
        {
            this.uiRivivePanel.HidePanel();
            this.onGameOver();
        };

        uiHpGauge.Init();
        uiWeaponLevelUp.Init();
        uiRivivePanel.Init();
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

    public void ShowRivivePanel()
    {
        this.uiRivivePanel.ShowPanel();
    }

    public void RunTimer()
    {
        this.uiRivivePanel.RiviveTimer();
    }
}
