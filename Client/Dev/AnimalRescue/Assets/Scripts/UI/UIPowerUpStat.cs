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
        item.Init("공격력","damage",heroId, data.increase_damage, info.dicHeroInfo[heroId].dicStats["damage"] * price);
        item.onClickLevelUp = (statkey) =>
        {
            this.onClickLevelUp(statkey);
        };

        itemGo = Instantiate(this.uiPowerUpStatItemPrefab, this.content);
        item = itemGo.GetComponent<UIPowerUpStatItem>();
        item.Init("체력", "maxhp", heroId, (int)data.increase_maxhp, info.dicHeroInfo[heroId].dicStats["maxhp"] * price);
        item.onClickLevelUp = (statkey) =>
        {
            this.onClickLevelUp(statkey);
        };

        itemGo = Instantiate(this.uiPowerUpStatItemPrefab, this.content);
        item = itemGo.GetComponent<UIPowerUpStatItem>();
        item.Init("이동속도","movespeed", heroId, data.increase_movespeed, info.dicHeroInfo[heroId].dicStats["movespeed"] * price);
        item.onClickLevelUp = (statkey) =>
        {
            this.onClickLevelUp(statkey);
        };
    }
}
