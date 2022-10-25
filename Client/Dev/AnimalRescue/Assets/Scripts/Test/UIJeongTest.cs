using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIJeongTest : MonoBehaviour
{
    private UIHpGauge uiHpGauge;
    public UIWeaponLevelUp uiWeaponLevelUp;
    public UnityAction<int> onWeaponSelect;

    public void Init()
    {
        this.uiHpGauge = this.transform.Find("UIHpGauge").GetComponent<UIHpGauge>();
        
        uiWeaponLevelUp.onWeaponSelect = (id) =>
        {
            this.onWeaponSelect(id);
            this.uiWeaponLevelUp.HideUI();
        };

        uiWeaponLevelUp.Init();
        this.uiHpGauge.Init();
    }

    public void ShowWeaponLevelUp()
    {
        this.uiWeaponLevelUp.ShowUI();
    }

    public void UpdatePosition(Vector3 worldPos)
    {
        this.uiHpGauge.UpdatePosition(worldPos);
    }

    public void UpdateUIHpGauge(float hp, float maxHp)
    {
        this.uiHpGauge.UpdateUI(hp, maxHp);
    }
}
