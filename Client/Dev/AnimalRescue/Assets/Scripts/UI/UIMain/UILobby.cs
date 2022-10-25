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

    public Text gold;
    public Text diamond;

    public UnityAction<eBtnLobby> onClickBtn;
    public void Init()
    {
        //GameObject btnGroup = transform.Find("BtnGroup").gameObject;
        //this.btnGameStart = btnGroup.transform.Find("BtnGameStart").GetComponent<Button>();
        //this.btnShop = btnGroup.transform.Find("BtnShop").GetComponent<Button>();
        //this.btnOption = btnGroup.transform.Find("BtnOption").GetComponent<Button>();
        //this.btnExit = btnGroup.transform.Find("BtnExit").GetComponent<Button>();

        this.btnGameStart.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.GameStart);
        });
        //this.btnShop.onClick.AddListener(() =>
        //{
        //    this.onClickBtn(eBtnLobby.Shop);
        //});
        this.btnRepairShop.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.RepairShop);
        });
        //this.btnOption.onClick.AddListener(() =>
        //{
        //    this.onClickBtn(eBtnLobby.Option);
        //});
        //this.btnExit.onClick.AddListener(() =>
        //{
        //    this.onClickBtn(eBtnLobby.Exit);
        //});
        var info = InfoManager.instance.GetInfo();
        this.gold.text = info.playerInfo.gold.ToString();
        this.diamond.text = info.playerInfo.diamond.ToString();
    }
}
