using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIBase : MonoBehaviour
{
    public GameObject panelOptionGo;
    public Button btnOption;
    public Button btnOptionClose;
    public UnityAction<bool> isShowPanelOption;
    public virtual void Init()
    {

    }

    public virtual void Init(int heroId)
    {

    }

    public void UIOptionInit()
    {
        this.btnOption.onClick.AddListener(() =>
        {
            ShowPanelOption();
        });
        this.btnOptionClose.onClick.AddListener(() =>
        {
            HidePanelOption();
        });
    }

    public void ShowPanelOption()
    {
        isShowPanelOption(true);
        panelOptionGo.SetActive(true);
    }

    public void HidePanelOption()
    {
        isShowPanelOption(false);
        panelOptionGo.SetActive(false);
    }
}
