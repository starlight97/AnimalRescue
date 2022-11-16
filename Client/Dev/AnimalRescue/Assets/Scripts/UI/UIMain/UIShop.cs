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
    public Button btnCloudSave;
    public Button btnCloudLoad;


    override public void Init()
    {
        base.Init();
        this.UIOptionInit();
        this.uiHeroShop = GameObject.FindObjectOfType<UIHeroShop>();


        this.uiHeroShop.Init(0);

        this.btnBack.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
            this.onClickLobby();
        });
        this.btnShowAd.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
            onClickAdsBtn();
        });
        this.isShowPanelOption = (check) =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
        };

        this.btnCloudSave.onClick.AddListener(() =>
        {
            GPGSManager.instance.SaveToCloud(InfoManager.instance.GetInfo());
        });
        this.btnCloudLoad.onClick.AddListener(() =>
        {
            GPGSManager.instance.LoadFromCloud();
        });
    }

    public Text GetTextGold()
    {
        return uiHeroShop.textGold;
    }
}
