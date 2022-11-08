using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIPowerUpStat : MonoBehaviour
{
    private RectTransform content;
    public GameObject uiPowerUpStatItemPrefab;
    public UnityAction<string> onClickLevelUp;

    private int price = 100;

    public void Init(int heroId)
    {
        this.content = transform.Find("Contents").GetComponent<RectTransform>();
        var info = InfoManager.instance.GetInfo();
        var data = DataManager.instance.GetData<HeroData>(heroId);

        GameObject itemGo = Instantiate(this.uiPowerUpStatItemPrefab, this.content);
        var item = itemGo.GetComponent<UIPowerUpStatItem>();
        item.Init("Damage","damage",heroId, data.increase_damage, info.dicHeroInfo[heroId].dicStats["damage"] * price);
        item.onClickLevelUp = (statkey) =>
        {
            this.onClickLevelUp(statkey);
        };

        itemGo = Instantiate(this.uiPowerUpStatItemPrefab, this.content);
        item = itemGo.GetComponent<UIPowerUpStatItem>();
        item.Init("Hp", "maxhp", heroId, (int)data.increase_maxhp, info.dicHeroInfo[heroId].dicStats["maxhp"] * price);
        item.onClickLevelUp = (statkey) =>
        {
            this.onClickLevelUp(statkey);
        };

        itemGo = Instantiate(this.uiPowerUpStatItemPrefab, this.content);
        item = itemGo.GetComponent<UIPowerUpStatItem>();
        item.Init("MoveSpeed","movespeed", heroId, data.increase_movespeed, info.dicHeroInfo[heroId].dicStats["movespeed"] * price);
        if(info.dicHeroInfo[heroId].dicStats["movespeed"] >= 100)
        {
            item.ShowMaxBtn();
        }
        item.onClickLevelUp = (statkey) =>
        {
            if (info.dicHeroInfo[heroId].dicStats["movespeed"] >= 100)
            {
                item.ShowMaxBtn();
            }
            this.onClickLevelUp(statkey);
        };
    }
}
