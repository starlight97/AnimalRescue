using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D;
using UnityEngine.Events;

public class UIHeroShop : MonoBehaviour
{
    public enum eConditionType
    {
        KILL = 0
    }
    public SpriteAtlas atlas;
    public RectTransform content;
    public GameObject uiHeroShopItemPrefab;
    public UnityAction<int> onClickPurchaseHero;
    public Text textGold;
    public AudioClip btnAudio;

    public void Init(int category)
    {
        var shopdatas = DataManager.instance.GetDataList<ShopgroupData>().ToList().FindAll(x => x.category == category);

        var info = InfoManager.instance.GetInfo();
        textGold.text = info.playerInfo.gold.ToString();
        // 이미 보유한 영웅은 상점 리스트에서 삭제
        foreach (var hero in info.dicHeroInfo.Values)
        {
            var haveHero = shopdatas.Find(x => x.item_id == hero.id);
            shopdatas.Remove(haveHero);
        }
        foreach (var shopdata in shopdatas)
        {
            AddItem(shopdata.id);
        }

        var hiddenShopdatas = DataManager.instance.GetDataList<ShopgroupData>().ToList().FindAll(x => x.category == 1);
        var herounlockDatas = DataManager.instance.GetDataList<HerounlockData>().ToList();
        foreach (var hiddenShopdata in hiddenShopdatas)
        {
            var herounlockData = herounlockDatas.Find(x => x.hero_id == hiddenShopdata.item_id);
            if (herounlockData.condition_type == (int)eConditionType.KILL)
            {
                
                if (info.dicKillEnemyInfo.ContainsKey(herounlockData.condition_key) == true)
                {
                    if(info.dicKillEnemyInfo[herounlockData.condition_key] >= herounlockData.condition_amount)
                    {
                        if(info.dicHeroInfo.ContainsKey(hiddenShopdata.item_id) == false)
                        {
                            AddItem(hiddenShopdata.id);
                        }
                    }
                }
            }
        }
    }

    private void AddItem(int id)
    {
        var shopdata = DataManager.instance.GetData<ShopgroupData>(id);
        var herodata = DataManager.instance.GetData<HeroData>(shopdata.item_id);
        GameObject itemGo = Instantiate(this.uiHeroShopItemPrefab, this.content);
        var item = itemGo.GetComponent<UIHeroShopItem>();
        item.Init(shopdata.item_id, herodata.hero_name, shopdata.price, atlas.GetSprite(herodata.sprite_name));
        item.btnPurchase.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySound(btnAudio);
            var info = InfoManager.instance.GetInfo();
            if (info.playerInfo.gold >= item.price)
            {
                info.playerInfo.gold -= item.price;
                Debug.Log(item.id + "구매");
                Destroy(itemGo);
                HeroInfo heroInfo = new HeroInfo(item.id);
                info.dicHeroInfo.Add(item.id, heroInfo);
                InfoManager.instance.SaveGame();
                textGold.text = info.playerInfo.gold.ToString();
            }
            else
            {
                Debug.Log("골드 부족");
            }
        });
    }
}
