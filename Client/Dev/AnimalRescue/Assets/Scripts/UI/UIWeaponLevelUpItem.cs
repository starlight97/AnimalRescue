using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIWeaponLevelUpItem : MonoBehaviour
{
    public int id;
    private Button btn;
    private Text textWeaponName;
    private Text textWeaponLevel;
    private Image imgWeapon;
    public UnityAction<int> onSelect;

    public void Init()
    {
        this.textWeaponName = transform.Find("TextWeaponName").GetComponent<Text>();
        this.textWeaponLevel = transform.Find("TextWeaponLevel").GetComponent<Text>();
        this.imgWeapon = transform.Find("WeaponImage").GetComponent<Image>();
        this.btn = this.GetComponent<Button>();
        this.btn.onClick.AddListener(() =>
        {
            this.onSelect(id);
        });
    }

    public void Setting(int id, string weaponName, Sprite sprite, int level)
    {
        this.id = id;
        this.textWeaponName.text = weaponName;
        this.imgWeapon.sprite = sprite;
        this.imgWeapon.SetNativeSize();
        this.textWeaponLevel.text = "Level : " + level;
    }


}
