using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRepairShop : UIBase
{
    public Button btnBack;
    public UnityAction onClickLobby;
    public Text textPlayerGold;
    //public Text textPlayerDiamond;
    public GameObject heroViewGo;
    public AudioSource audioSource;
    private int powerUpConut = 0;

    private UIPowerUpStat uiPowerUpStat;
    private UIHeroDetailStats uiHeroDetailStats;

    public ParticleSystem heartsParticleGo;
    public ParticleSystem starParticleGo;
    override public void Init(int heroId)
    {
        base.Init(heroId);
        base.UIOptionInit();
        var info = InfoManager.instance.GetInfo();
        this.textPlayerGold.text = info.playerInfo.gold.ToString();
        //this.textPlayerDiamond.text = info.playerInfo.diamond.ToString();

        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });

        this.uiPowerUpStat = GameObject.FindObjectOfType<UIPowerUpStat>();
        this.uiHeroDetailStats = GameObject.FindObjectOfType<UIHeroDetailStats>();
        this.uiPowerUpStat.Init(heroId);
        this.uiHeroDetailStats.Init(heroId);

        var heroData = DataManager.instance.GetData<HeroData>(heroId);
        var uiHeroGo = Instantiate(Resources.Load<GameObject>(heroData.ui_prefab_path), heroViewGo.transform);
        var uiHero = uiHeroGo.GetComponent<UIHero>();
        uiHero.Init();

        uiPowerUpStat.onClickLevelUp = (statType) =>
        {
            audioSource.Play();
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
            heroViewGo.SetActive(!check);
        };

        this.uiHeroDetailStats.UpdateUI();
    }

}
