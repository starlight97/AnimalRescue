using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.Events;

public class UIHeroList : MonoBehaviour
{
    public SpriteAtlas atlas;
    private RectTransform content;
    public GameObject uiHeroListItemPrefab;
    public UnityAction<int> onCLickHero;

    private List<UIHeroListItem> uiHeroListItemList = new List<UIHeroListItem>();
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
            item.Init(hero.id, atlas.GetSprite(heroData.sprite_name), heroData.hero_name);

            item.btnHeroListItem.onClick.AddListener(() =>
            {
                this.onCLickHero(hero.id);
            });
            uiHeroListItemList.Add(item);
        }
        this.onCLickHero(uiHeroListItemList[0].id);       

    }
}
