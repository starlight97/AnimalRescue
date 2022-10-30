using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIHeroShop : MonoBehaviour
{
    public RectTransform content;
    public GameObject uiHeroShopItemPrefab;
    public UnityAction<int> onClickPurchaseHero;

    public void Init(int category)
    {
        var shopdatas = DataManager.instance.GetDataList<ShopgroupData>().ToList().FindAll(x => x.category == category);

        var info = InfoManager.instance.GetInfo();

        // 이미 보유한 영웅은 상점 리스트에서 삭제
        foreach (var hero in info.dicHeroInfo.Values)
        {
            var haveHero = shopdatas.Find(x => x.item_id == hero.id);
            shopdatas.Remove(haveHero);
        }
        foreach (var shopdata in shopdatas)
        {
            var herodata = DataManager.instance.GetData<HeroData>(shopdata.item_id);
            GameObject itemGo = Instantiate(this.uiHeroShopItemPrefab, this.content);
            var item = itemGo.GetComponent<UIHeroShopItem>();
            item.Init(shopdata.item_id, herodata.hero_name, shopdata.price);
            item.btnPurchase.onClick.AddListener(() =>
            {
                Debug.Log(item.id + "구매");
                Destroy(itemGo);
                //this.onClickPurchaseHero(item.id);
            });
        }
    }
}
