using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UILobby : MonoBehaviour
{
    public enum eBtnLobby
    {
        GameReady,
        Shop,
        Option,
        Exit
    }
    private Button btnGameReady;
    private Button btnShop;
    private Button btnOption;
    private Button btnExit;

    public UnityAction<eBtnLobby> onClickBtn;
    public void Init()
    {
        GameObject btnGroup = transform.Find("BtnGroup").gameObject;
        this.btnGameReady = btnGroup.transform.Find("BtnGameReady").GetComponent<Button>();
        this.btnShop = btnGroup.transform.Find("BtnShop").GetComponent<Button>();
        this.btnOption = btnGroup.transform.Find("BtnOption").GetComponent<Button>();
        this.btnExit = btnGroup.transform.Find("BtnExit").GetComponent<Button>();

        this.btnGameReady.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.GameReady);
        });
        this.btnShop.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.Shop);
        });
        this.btnOption.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.Option);
        });
        this.btnExit.onClick.AddListener(() =>
        {
            this.onClickBtn(eBtnLobby.Exit);
        });
    }
}
