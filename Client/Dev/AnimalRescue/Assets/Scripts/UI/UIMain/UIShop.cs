using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIShop : UIBase
{
    private UIHeroShop uiHeroShop;
    public UnityAction onClickLobby;
    public UnityAction onClickAdsBtn;
    public Button btnBack;
    public Button btnShowAd;
    override public void Init()
    {
        base.Init();
        this.uiHeroShop = GameObject.FindObjectOfType<UIHeroShop>();



        this.uiHeroShop.Init(0);

        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });
        btnShowAd.onClick.AddListener(() =>
        {
            onClickAdsBtn();
        });
    }

    public Text GetTextGold()
    {
        return uiHeroShop.textGold;
    }
}
