using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboutPanel : MonoBehaviour
{
    public GameObject popupAbout;
    public Button btnExit;
    public UIAboutScrollRect uiAboutScrollView;

    public void Init()
    {
        popupAbout.gameObject.SetActive(false);

        this.btnExit.onClick.AddListener(() =>
        {
            uiAboutScrollView.ResetStartPos();
            popupAbout.gameObject.SetActive(false);
        });
        this.uiAboutScrollView.Init();
    }

    public void ShowPanel()
    {
        this.popupAbout.gameObject.SetActive(true);
        this.uiAboutScrollView.ScrollText();
    }
}
