using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIShopNoticePopup : MonoBehaviour
{
    private RectTransform noticePopupGo;
    private Button btnOk;
    public UnityAction onClickBtn;
    public UnityAction onShowUI;

    public void Init()
    {
        this.noticePopupGo = this.transform.Find("NoticePopup").GetComponent<RectTransform>();
        this.btnOk = this.transform.Find("NoticePopup").transform.Find("OkBtn").GetComponent<Button>();

        this.btnOk.onClick.AddListener(() => {
            this.transform.gameObject.SetActive(false);
            onClickBtn();
        });
    }

    public void ShowUI()
    {
        this.noticePopupGo.gameObject.SetActive(true);
        onShowUI();
    }
}
