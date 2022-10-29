using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRepairShop : MonoBehaviour
{
    public Button btnBack;
    public UnityAction onClickLobby;
    public Text textPlayerGold;
    public Text textPlayerDiamond;
    public GameObject heroViewGo;

    private UIPowerUpStat uiPowerUpStat;
    private UIHeroDetailStats uiHeroDetailStats;
    public void Init(int heroId)
    {
        var info = InfoManager.instance.GetInfo();
        this.textPlayerGold.text = info.playerInfo.gold.ToString();
        this.textPlayerDiamond.text = info.playerInfo.diamond.ToString();

        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });

        this.uiPowerUpStat = GameObject.FindObjectOfType<UIPowerUpStat>();
        this.uiHeroDetailStats = GameObject.FindObjectOfType<UIHeroDetailStats>();
        this.uiPowerUpStat.Init(heroId);
        this.uiHeroDetailStats.Init(heroId);
        uiPowerUpStat.onClickLevelUp = (statType) =>
        {
            this.uiHeroDetailStats.UpdateUI();
            this.textPlayerGold.text = info.playerInfo.gold.ToString();
            this.textPlayerDiamond.text = info.playerInfo.diamond.ToString();
        };

        var heroData = DataManager.instance.GetData<HeroData>(heroId);
        var uiHeroGo = Instantiate(Resources.Load<GameObject>(heroData.ui_prefab_path), heroViewGo.transform);
        this.uiHeroDetailStats.UpdateUI();
    }
}
