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
        this.UIOptionInit();
        this.uiHeroShop = GameObject.FindObjectOfType<UIHeroShop>();


        this.uiHeroShop.Init(0);

        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });
        this.btnShowAd.onClick.AddListener(() =>
        {
            onClickAdsBtn();
        });
        this.isShowPanelOption = (check) =>
        {

        };
    }

    public Text GetTextGold()
    {
        return uiHeroShop.textGold;
    }
}
