using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILobby : UIBase
{
    public enum eBtnLobby
    {
        GameStart,
        Shop,
        RepairShop,
        Option,
        OptionClose,
        Exit
    }
    public Button btnGameStart;
    public Button btnShop;
    public Button btnRepairShop;

    public Button btnCloudSave;
    public Button btnCloudLoad;
    private Button btnExit;

    private UIHeroList uiHeroList;
    private UILobbyHeroStats uiLobbyHeroStats;
    private UIAboutPanel uiAboutPanel;
    private UIRotateDots uiRotateDots;

    public Text gold;
    public Text diamond;

    public UnityAction<eBtnLobby> onClickBtn;
    public UnityAction<int> onClickHero;

    public Button btnLoadCheck;
    public Button btnSaveCheck;
    public GameObject panelSaveCheckGo;
    public GameObject panelLoadCheckGo;
    public UnityAction onDataLoadComplete;

    public Button btnReview;
    public Button btnAbout;

    override public void Init()
    {
        base.Init();
        base.UIOptionInit();

        this.uiLobbyHeroStats = GameObject.FindObjectOfType<UILobbyHeroStats>();
        this.uiHeroList = GameObject.FindObjectOfType<UIHeroList>();
        this.uiAboutPanel = GameObject.FindObjectOfType<UIAboutPanel>();
        this.uiRotateDots = GameObject.FindObjectOfType<UIRotateDots>();

        this.btnGameStart.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.GameStart);
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
        });
        this.btnShop.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.Shop);
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
        });
        this.btnRepairShop.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.RepairShop);
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
        });
        this.isShowPanelOption = (check) =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);

            if (check)
                this.onClickBtn(eBtnLobby.Option);
            else
                this.onClickBtn(eBtnLobby.OptionClose);
        };
        this.btnCloudSave.onClick.AddListener(() =>
        {
            uiRotateDots.Show();
            GPGSManager.instance.SaveToCloud(InfoManager.instance.GetInfo());            
        });
        this.btnCloudLoad.onClick.AddListener(() =>
        {
            uiRotateDots.Show();
            GPGSManager.instance.LoadFromCloud();
        });

        this.uiHeroList.onCLickHero = (id) =>
        {
            this.onClickHero(id);
        };

        this.btnReview.onClick.AddListener(() =>
        {
            Application.OpenURL("market://details?id=com.subingo.animalrescue");
        });

        this.btnSaveCheck.onClick.AddListener(() =>
        {
            uiRotateDots.Hide();
            panelSaveCheckGo.gameObject.SetActive(false);
        });
        this.btnLoadCheck.onClick.AddListener(() =>
        {
            uiRotateDots.Hide();
            onDataLoadComplete();
        });
        this.btnAbout.onClick.AddListener(() => 
        {
            this.uiAboutPanel.ShowPanel();        
        });
        this.uiLobbyHeroStats.Init();
        this.uiHeroList.Init();
        this.uiAboutPanel.Init();
        this.uiRotateDots.Init();

        var info = InfoManager.instance.GetInfo();
        this.gold.text = info.playerInfo.gold.ToString();
        this.diamond.text = info.playerInfo.diamond.ToString();

        GPGSManager.instance.onSavedCloud = () =>
        {
            uiRotateDots.Hide();
            panelSaveCheckGo.gameObject.SetActive(true);
            //this.textGameInfo.text = status.ToString();
        };
        GPGSManager.instance.onLoadedCloud = (info) =>
        {
            uiRotateDots.Hide();
            panelLoadCheckGo.gameObject.SetActive(true);
            InfoManager.instance.SetInfo(info);
            //var json = JsonConvert.SerializeObject(this.gameInfo);
        };
    }

    public void UiLobbyHeroStatsUIUpdate(string heroName, int damage, int hp, float moveSpeed)
    {
        this.uiLobbyHeroStats.UIUpdate(heroName, damage, hp, moveSpeed);
    }
}
