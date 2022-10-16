using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIWeaponLevelUpItem : MonoBehaviour
{
    public int id;
    private Button btn;
    public UnityAction<int> onSelect;

    public void Init()
    {
        this.btn = this.GetComponent<Button>();
        this.btn.onClick.AddListener(() =>
        {
            this.onSelect(id);
        });
    }

    public void Setting(int id)
    {
        this.id = id;
    }
    public void Setting()
    {
        
    }

}
