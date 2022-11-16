using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICloudPopups : MonoBehaviour
{
    public GameObject savePopupGo;
    public GameObject loadPopupGo;
    public Button saveOkBtn;
    public Button loadOkBtn;

    public void Init()
    {
        this.savePopupGo = this.transform.Find("Save").GetComponent<GameObject>();
        this.loadPopupGo = this.transform.Find("Load").GetComponent<GameObject>();

        saveOkBtn.onClick.AddListener(() => {
            this.savePopupGo.gameObject.SetActive(false);
        });

        loadOkBtn.onClick.AddListener(() => {
            this.loadOkBtn.gameObject.SetActive(false);
        });
    }

    public void ShowSavePopup()
    {
        this.savePopupGo.gameObject.SetActive(true);
    }

    public void ShowLoadPopup()
    {
        this.loadPopupGo.gameObject.SetActive(true);
    }
}
