using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRepairShop : UIBase
{
    public enum eBtnRepairShop
    {
        Back,
        Shop
    }

    public Button btnAbout;
    public Button btnReview;
    public Button btnCloudSave;
    public Button btnCloudLoad;
    public Button btnBack;
    public Button btnShop;
    public Button btnLoadCheck;
    public Button btnSaveCheck;
    public GameObject panelCloudGo;
    public UnityAction<eBtnRepairShop> onClickBtn;
    public UnityAction onDataLoadComplete;
    public Text textPlayerGold;
    //public Text textPlayerDiamond;
    public GameObject heroViewGo;
    private int powerUpConut = 0;

    private UIPowerUpStat uiPowerUpStat;
    private UIHeroDetailStats uiHeroDetailStats;
    private UIAboutPanel uiAboutPanel;

    public ParticleSystem heartsParticleGo;
    public ParticleSystem starParticleGo;

    public GameObject panelSaveCheckGo;
    public GameObject panelLoadCheckGo;

    override public void Init(int heroId)
    {
        base.Init(heroId);
        base.UIOptionInit();
        var info = InfoManager.instance.GetInfo();
        this.textPlayerGold.text = info.playerInfo.gold.ToString();
        //this.textPlayerDiamond.text = info.playerInfo.diamond.ToString();

        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnRepairShop.Back);
        });
        this.btnShop.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnRepairShop.Shop);
        });
        this.btnReview.onClick.AddListener(() =>
        {
            Application.OpenURL("market://details?id=com.subingo.animalrescue");
        });
        this.btnAbout.onClick.AddListener(() =>
        {
            this.uiAboutPanel.ShowPanel();
        });
        this.uiPowerUpStat = GameObject.FindObjectOfType<UIPowerUpStat>();
        this.uiHeroDetailStats = GameObject.FindObjectOfType<UIHeroDetailStats>();
        this.uiAboutPanel = GameObject.FindObjectOfType<UIAboutPanel>();
        this.uiPowerUpStat.Init(heroId);
        this.uiHeroDetailStats.Init(heroId);
        this.uiAboutPanel.Init();

        var heroData = DataManager.instance.GetData<HeroData>(heroId);
        var uiHeroGo = Instantiate(Resources.Load<GameObject>(heroData.ui_prefab_path), heroViewGo.transform);
        var uiHero = uiHeroGo.GetComponent<UIHero>();
        uiHero.Init();

        uiPowerUpStat.onClickLevelUp = (statType) =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button3);
            this.uiHeroDetailStats.UpdateUI();
            this.textPlayerGold.text = info.playerInfo.gold.ToString();
            //this.textPlayerDiamond.text = info.playerInfo.diamond.ToString();
            powerUpConut++;
            if(powerUpConut % 3 == 0)
            {
                uiHero.SetAnim(UIHero.eState.PowerUp02);
                heartsParticleGo.Play();
            }
            else
            {
                uiHero.SetAnim(UIHero.eState.PowerUp01);
                starParticleGo.Play();
            }

        };
        this.isShowPanelOption = (check) =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
            heroViewGo.SetActive(!check);
        };

        this.btnCloudSave.onClick.AddListener(() =>
        {
            GPGSManager.instance.SaveToCloud(InfoManager.instance.GetInfo());
        });
        this.btnCloudLoad.onClick.AddListener(() =>
        {
            GPGSManager.instance.LoadFromCloud();
        });

        GPGSManager.instance.onSavedCloud = () =>
        {
            panelSaveCheckGo.gameObject.SetActive(true);
            //this.textGameInfo.text = status.ToString();
        };
        GPGSManager.instance.onLoadedCloud = (info) =>
        {
            panelLoadCheckGo.gameObject.SetActive(true);
            InfoManager.instance.SetInfo(info);
            //var json = JsonConvert.SerializeObject(this.gameInfo);
        };

        this.btnSaveCheck.onClick.AddListener(() =>
        {
            panelSaveCheckGo.gameObject.SetActive(false);
        });
        this.btnLoadCheck.onClick.AddListener(() =>
        {
            onDataLoadComplete();
        });
        this.uiHeroDetailStats.UpdateUI();
    }
}
