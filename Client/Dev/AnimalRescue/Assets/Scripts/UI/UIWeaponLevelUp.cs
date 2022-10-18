using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIWeaponLevelUp : MonoBehaviour
{
    public UnityAction<int> onWeaponSelect;
    public UIWeaponLevelUpItem[] uiWeaponLevelUpItems;

    public void Init()
    {
        this.HideUI();
        foreach (var item in uiWeaponLevelUpItems)
        {
            item.Init();
            item.onSelect = (id) =>
            {
                this.onWeaponSelect(id);                
            };
        }
    }

    public void ShowUI()
    {
        this.gameObject.SetActive(true);
        foreach (var item in uiWeaponLevelUpItems)
        {
            item.Setting();
        }
    }
    public void HideUI()
    {
        this.gameObject.SetActive(false);

    }
}
