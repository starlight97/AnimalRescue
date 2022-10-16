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
        foreach (var item in uiWeaponLevelUpItems)
        {
            item.Init();
            item.onSelect = (id) =>
            {
                this.onWeaponSelect(id);
                this.HideItems();
            };
        }
    }

    public void ShowItems()
    {
        foreach (var item in uiWeaponLevelUpItems)
        {
            item.Setting();
            item.gameObject.SetActive(true);
        }
    }
    public void HideItems()
    {
        foreach (var item in uiWeaponLevelUpItems)
        {
            item.Setting();
            item.gameObject.SetActive(false);
        }
    }
}
