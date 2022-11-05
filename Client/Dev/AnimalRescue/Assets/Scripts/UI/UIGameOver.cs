using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIGameOver : UIBase
{
    public enum eBtnType
    {
        Again, Lobby
    }

    public Button btnAgain;
    public Button btnLobby;
    public UnityAction<eBtnType> onClickBtn;

    public override void Init()
    {
        base.Init();

        btnAgain.onClick.AddListener(() => {
            this.onClickBtn(eBtnType.Again);
        });

        btnLobby.onClick.AddListener(() => {
            this.onClickBtn(eBtnType.Lobby);
        });
    }
}
