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
            SetActivePanelOption(true);
        });
        this.btnOptionClose.onClick.AddListener(() =>
        {
            SetActivePanelOption(false);
        });

        this.isShowPanelOption = (check) =>
        {
        };
    }

    // state : true 호출시 Panel 켜기
    // state : false 호출시 Panel 끄기
    public void SetActivePanelOption(bool state)
    {
        isShowPanelOption(state);
        panelOptionGo.SetActive(state);
    }

}
