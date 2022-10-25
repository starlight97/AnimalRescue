using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIGame : MonoBehaviour
{
    private UIHpGauge uiHpGauge;
    private UIWeaponLevelUp uiWeaponLevelUp;
    public UnityAction<int> onWeaponSelect;

    public void Init()
    {
        this.uiHpGauge = this.transform.Find("UIHpGauge").GetComponent<UIHpGauge>();
        this.uiWeaponLevelUp = this.transform.Find("UIWeaponLevelUp").GetComponent<UIWeaponLevelUp>();
        
        uiWeaponLevelUp.onWeaponSelect = (id) =>
        {
            this.onWeaponSelect(id);
            this.uiWeaponLevelUp.HideUI();
        };

        uiHpGauge.Init();
        uiWeaponLevelUp.Init();
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
}
