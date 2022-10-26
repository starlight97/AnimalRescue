using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPowerUpStatItem : MonoBehaviour
{
    private Text textStatName;
    private Text textIncrease;
    private Text textPrice;

    public Button btnLevelUp;
    private int price;
    private string statkey;
    private int heroId;

    public void Init(string statType,string statkey, int heroId, int increase, int price)
    {
        this.textIncrease = transform.Find("TextIncrease").GetComponent<Text>();
        this.btnLevelUp = transform.Find("BtnLevelUp").GetComponent<Button>();
        this.textStatName = transform.Find("TextStatName").GetComponent<Text>();
        this.textPrice = this.btnLevelUp.transform.Find("TextPrice").GetComponent<Text>();

        this.textStatName.text = statType;
        this.textIncrease.text = "+" + increase;
        this.textPrice.text = price.ToString();
        this.price = price;
        this.statkey = statkey;
        this.heroId = heroId;
    }

    public bool LevelUp()
    {
        var info = InfoManager.instance.GetInfo();
        var gold = info.playerInfo.gold;

        if(gold >= this.price)
        {
            info.playerInfo.gold -= this.price;

            this.price += StatsConstants.StatpowerUpPrice;
            this.textPrice.text = price.ToString();

            info.dicHeroInfo[heroId].dicStats[statkey]++;
            InfoManager.instance.SaveGame();
            return true;
        }
        else
        {
            Debug.Log("골드 부족");
            return false;
        }
    }

}
