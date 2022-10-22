using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIWeaponLevelUp : MonoBehaviour
{
    public UnityAction<int> onWeaponSelect;

    public UIWeaponLevelUpItem[] uiWeaponLevelUpItems;
    private List<WeaponData> weaponDataList;
    private WeaponManager weaponManager;
    public void Init()
    {
        this.HideUI();
        weaponDataList = new List<WeaponData>();
        weaponManager = GameObject.FindObjectOfType<WeaponManager>();
        weaponDataList = DataManager.instance.GetDataList<WeaponData>().ToList();


        foreach (var item in uiWeaponLevelUpItems)
        {
            item.Init();
            item.onSelect = (id) =>
            {
                this.onWeaponSelect(id);                
            };
        }
    }

    public void WeaponLevelUpItemsSetting()
    {
        List<int> weaponIdList = new List<int>();
        foreach (var data in weaponDataList)
        {
            weaponIdList.Add(data.id);
        }

        foreach (var item in uiWeaponLevelUpItems)
        {
            var randIdx = Random.Range(0, weaponIdList.Count);
            var data = DataManager.instance.GetData<WeaponData>(weaponIdList[randIdx]);

            int id = weaponIdList[randIdx];
            int level = weaponManager.GetWeaponLevel(data.id);
            string weapon_name = data.weapon_name;
            item.Setting(id, weapon_name, level);
            weaponIdList.RemoveAt(randIdx);
        }
    }

    public void ShowUI()
    {
        WeaponLevelUpItemsSetting();
        this.gameObject.SetActive(true);

    }
    public void HideUI()
    {
        this.gameObject.SetActive(false);

    }
}
