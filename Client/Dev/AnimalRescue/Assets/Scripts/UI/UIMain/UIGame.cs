using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIGame : UIBase
{
    private Image dimImage;
    private UIHpGauge uiHpGauge;
    private UIWeaponLevelUp uiWeaponLevelUp;
    private UIRivivePanel uiRivivePanel;
    public UnityAction<int> onWeaponSelect;
    public UnityAction onGameOver;
    public UnityAction onClickAds;

    public override void Init()
    {
        base.Init();
        this.dimImage = this.transform.Find("dim").GetComponent<Image>();
        this.uiHpGauge = this.transform.Find("UIHpGauge").GetComponent<UIHpGauge>();
        this.uiWeaponLevelUp = this.transform.Find("UIWeaponLevelUp").GetComponent<UIWeaponLevelUp>();
        this.uiRivivePanel = this.transform.Find("UIRivivePanel").GetComponent<UIRivivePanel>();

        this.dimImage.gameObject.SetActive(false);
        
        uiWeaponLevelUp.onWeaponSelect = (id) =>
        {
            this.onWeaponSelect(id);
            this.uiWeaponLevelUp.HideUI();
            this.dimImage.gameObject.SetActive(false);
        };

        #region RiviveAction
        uiRivivePanel.onClickNoBtn = () => 
        {
            this.uiRivivePanel.HidePanel();
            this.onGameOver();
        };

        uiRivivePanel.onClickAdsBtn = () =>
        {
            this.uiRivivePanel.HidePanel();
            this.onClickAds();
        };

        uiRivivePanel.onTimeOver = () => 
        {
            this.uiRivivePanel.HidePanel();
            this.onGameOver();
        };
        #endregion

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
        this.dimImage.gameObject.SetActive(true);
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
