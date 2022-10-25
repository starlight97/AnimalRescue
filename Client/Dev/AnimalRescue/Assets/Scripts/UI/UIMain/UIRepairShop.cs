using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRepairShop : MonoBehaviour
{
    public Button btnBack;
    public UnityAction onClickLobby;
    public void Init()
    {
        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });
    }
}
