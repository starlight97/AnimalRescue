using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMain : SceneMain
{
    private UILoading uiLoading;
    public override void Init(SceneParams param = null)
    {
        this.uiLoading = GameObject.FindObjectOfType<UILoading>();
        uiLoading.Init();

        DataManager.instance.onDataLoadComplete.AddListener((dataName, progress) =>
        {
            uiLoading.SetUI(dataName, progress);
        });

        DataManager.instance.onDataLoadFinished.AddListener(() =>
        {
            this.Dispatch("onLoadComplete");
        });
        DataManager.instance.Init();
        DataManager.instance.LoadAllData();

        InfoManager.instance.Init();
        AdMobManager.instance.Init();

    }
}
