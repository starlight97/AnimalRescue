using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIJeongTest : MonoBehaviour
{
    public UIHpGauge uiHpGauge;
    public UIWeaponLevelUp uiWeaponLevelUp;
    public UnityAction<int> onWeaponSelect;

    public void Init()
    {
        uiWeaponLevelUp.onWeaponSelect = (id) =>
        {
            this.onWeaponSelect(id);
            this.uiWeaponLevelUp.HideUI();
        };

        uiWeaponLevelUp.Init();
    }

    public void ShowWeaponLevelUp()
    {
        this.uiWeaponLevelUp.ShowUI();
    }
}
