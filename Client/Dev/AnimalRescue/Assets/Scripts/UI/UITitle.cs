using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITitle : MonoBehaviour
{
    private Button btnGameReady;
    private Button btnShop;
    private Button btnGameOption;

    public UnityAction<string> onClickBtn;
    public void Init()
    {
        this.btnGameReady.onClick.AddListener(() =>
        {
            this.onClickBtn("GameReady");
        });
        this.btnShop.onClick.AddListener(() =>
        {
            this.onClickBtn("Shop");
        });
    }
}
