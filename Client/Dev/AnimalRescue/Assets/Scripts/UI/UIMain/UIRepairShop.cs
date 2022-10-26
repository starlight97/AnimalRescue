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
    public Text textHeroName;

    private UIPowerUpStat uiPowerUpStat;
    public void Init(int heroId)
    {
        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });

        this.uiPowerUpStat = GameObject.FindObjectOfType<UIPowerUpStat>();
        uiPowerUpStat.Init(heroId);

        uiPowerUpStat.onCLickLevelUp = (statType) =>
        {

        };

        var heroData = DataManager.instance.GetData<HeroData>(heroId);        
        this.textHeroName.text = heroData.hero_name;
    }
}
