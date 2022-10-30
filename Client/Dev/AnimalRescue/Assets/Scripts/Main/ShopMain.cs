using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopMain : SceneMain
{
    private UIShop uiShop;

    private void Start()
    {
        //DataManager.instance.Init();
        //DataManager.instance.LoadAllData(this);
        //InfoManager.instance.Init();

        //DataManager.instance.onDataLoadFinished.AddListener(() =>
        //{
        //    this.Init();
        //});

    }

    public override void Init(SceneParams param = null)
    {
        base.Init(param);

        this.uiShop = GameObject.FindObjectOfType<UIShop>();

        this.uiShop.Init();

        this.uiShop.onClickLobby = () =>
        {
            Dispatch("onClickLobby");
        };
    }
}
