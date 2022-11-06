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
    public GameObject heroSpaceGo;
    public UnityAction<eBtnType> onClickBtn;

    public override void Init(int heroId)
    {
        base.Init(heroId);

        btnAgain.onClick.AddListener(() => {
            this.onClickBtn(eBtnType.Again);
        });

        btnLobby.onClick.AddListener(() => {
            this.onClickBtn(eBtnType.Lobby);
        });

        var data = DataManager.instance.GetData<HeroData>(heroId);
        var uiHeroGo = Instantiate(Resources.Load<GameObject>(data.ui_prefab_path), heroSpaceGo.transform);
        var uiHero = uiHeroGo.GetComponent<UIHero>();
        uiHero.Init();

        var uiHeroAnim = uiHero.GetComponent<Animator>();
        uiHeroAnim.SetTrigger(UIHero.eState.Dizzy.ToString());
    }
}
