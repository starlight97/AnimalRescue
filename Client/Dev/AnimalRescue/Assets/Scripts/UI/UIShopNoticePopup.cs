using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIShopNoticePopup : MonoBehaviour
{
    private Button btnOk;
    public UnityAction onClickBtn;

    public void Init()
    {
        this.btnOk = this.transform.Find("OkBtn").GetComponent<Button>();
        this.transform.gameObject.SetActive(false);

        this.btnOk.onClick.AddListener(() => {
            this.transform.gameObject.SetActive(false);
        });
    }

    public void ShowUI()
    {
        this.transform.gameObject.SetActive(true);
    }
}
