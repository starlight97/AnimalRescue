using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    public enum eBtnLobby
    {
        GameStart,
        Shop,
        RepairShop,
        Option,
        Exit
    }
    public Button btnGameStart;
    private Button btnShop;
    public Button btnRepairShop;
    private Button btnOption;
    private Button btnExit;

    private UIHeroList uiHeroList;
    private UILobbyHeroStats uiLobbyHeroStats;

    public Text gold;
    public Text diamond;

    public UnityAction<eBtnLobby> onClickBtn;
    public UnityAction<int> onClickHero;
    public void Init()
    {
        this.uiLobbyHeroStats = GameObject.FindObjectOfType<UILobbyHeroStats>();
        this.uiHeroList = GameObject.FindObjectOfType<UIHeroList>();

        this.btnGameStart.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.GameStart);
        });
        this.btnRepairShop.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.RepairShop);
        });
        this.uiHeroList.onCLickHero = (id) =>
        {
            this.onClickHero(id);
        };
        this.uiLobbyHeroStats.Init();
        this.uiHeroList.Init();

        var info = InfoManager.instance.GetInfo();
        this.gold.text = info.playerInfo.gold.ToString();
        this.diamond.text = info.playerInfo.diamond.ToString();
    }

    public void UiLobbyHeroStatsUIUpdate(string heroName, int damage, int hp, int moveSpeed)
    {
        this.uiLobbyHeroStats.UIUpdate(heroName, damage, hp, moveSpeed);
    }
}
