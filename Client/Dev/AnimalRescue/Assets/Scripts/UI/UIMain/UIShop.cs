using UnityEngine;
using UnityEngine.UI;

public class UIShop : MonoBehaviour
{
    private UIHeroShop uiHeroShop;
    public Button btnShowAd;
    public void Init()
    {
        this.uiHeroShop = GameObject.FindObjectOfType<UIHeroShop>();

        this.uiHeroShop.Init(0);
        
        btnShowAd.onClick.AddListener(() =>
        {
            AdMobManager.instance.ShowAds();
        });
    }
}
