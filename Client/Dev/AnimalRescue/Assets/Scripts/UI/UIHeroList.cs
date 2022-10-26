using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHeroList : MonoBehaviour
{
    private RectTransform content;
    public GameObject uiHeroListItemPrefab;
    //public 
    public void Init()
    {
        this.content = transform.Find("Contents").GetComponent<RectTransform>();
        var info = InfoManager.instance.GetInfo();

        foreach (var hero in info.dicHeroInfo.Values)
        {
            GameObject itemGo = Instantiate(this.uiHeroListItemPrefab, this.content);
            var item = itemGo.GetComponent<UIHeroListItem>();
            var heroData = DataManager.instance.GetData<HeroData>(hero.id);
            //item.Init(hero.id, heroData.heroname);
        }

        //foreach (var data in GoldShopMain.instance.dicGoldShopDatas.Values)
        //{
        //    Debug.LogFormat("{0} {1} {2} {3} {4}", data.id, data.name, data.price, data.sprite_name, data.dollar);
        //    GameObject itemGo = Instantiate(this.uiGoldShopItemPrefab, this.content);
        //    var item = itemGo.GetComponent<UIGoldShopItem>();
        //    Sprite sp = this.atlas.GetSprite(data.sprite_name);

        //    item.Init(data.id, data.name, data.price, data.dollar, sp);

        //    item.btnPurchase.onClick.AddListener(() =>
        //    {
        //        Debug.LogFormat("아이템 구매 : {0}", item.id);
        //    });
        //    i++;
        //}
    }
}
