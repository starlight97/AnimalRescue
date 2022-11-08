using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIPowerUpStatItem : MonoBehaviour
{
    private Text textStatName;
    private Text textIncrease;
    private Text textPrice;

    private Button btnLevelUp;
    private Button btnMax;
    private int price;
    private string statkey;
    private int heroId;

    public UnityAction<string> onClickLevelUp;

    public void Init(string statType,string statkey, int heroId, float increase, int price)
    {
        this.textIncrease = transform.Find("TextIncrease").GetComponent<Text>();
        this.btnLevelUp = transform.Find("BtnLevelUp").GetComponent<Button>();
        this.btnMax = transform.Find("BtnMax").GetComponent<Button>();
        this.textStatName = transform.Find("TextStatName").GetComponent<Text>();
        this.textPrice = this.btnLevelUp.transform.Find("TextPrice").GetComponent<Text>();

        this.textStatName.text = statType;
        this.textIncrease.text = "+" + increase;
        this.textPrice.text = price.ToString();
        this.price = price;
        this.statkey = statkey;
        this.heroId = heroId;

        this.btnLevelUp.onClick.AddListener(() =>
        {
            if (this.LevelUp())
                this.onClickLevelUp(statkey);
        });
        this.btnMax.onClick.AddListener(() =>
        {
            SoundManager.instance.PlaySound(SoundManager.eButtonAudio.Button1);
        });
        this.btnMax.gameObject.SetActive(false);
    }

    public void ShowMaxBtn()
    {
        btnLevelUp.gameObject.SetActive(false);
        btnMax.gameObject.SetActive(true);
    }

    private bool LevelUp()
    {
        var info = InfoManager.instance.GetInfo();
        var gold = info.playerInfo.gold;

        if(gold >= this.price)
        {
            info.playerInfo.gold -= this.price;

            this.price += GameConstants.StatpowerUpPrice;
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
