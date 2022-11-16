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

    private UIGameResult uiGameResult;
    private UIShopNoticePopup uiShopNoticePopup;

    public Button btnAgain;
    public Button btnLobby;
    public GameObject heroSpaceGo;
    private Animator uiHeroAnim;
    public UnityAction<eBtnType> onClickBtn;

    public override void Init(int heroId)
    {
        base.Init(heroId);

        this.uiGameResult = GameObject.Find("UIGameResult").GetComponent<UIGameResult>();
        this.uiShopNoticePopup = GameObject.FindObjectOfType<UIShopNoticePopup>();

        var data = DataManager.instance.GetData<HeroData>(heroId);
        var uiHeroGo = Instantiate(Resources.Load<GameObject>(data.ui_prefab_path), heroSpaceGo.transform);
        var uiHero = uiHeroGo.GetComponent<UIHero>();
        uiHero.Init();

        uiHeroAnim = uiHero.GetComponent<Animator>();
        uiHeroAnim.keepAnimatorControllerStateOnDisable = true;
        uiHeroAnim.SetTrigger(UIHero.eState.Dizzy.ToString());

        btnAgain.onClick.AddListener(() => {
            this.onClickBtn(eBtnType.Again);
        });

        btnLobby.onClick.AddListener(() => {
            this.onClickBtn(eBtnType.Lobby);
        });

        this.uiShopNoticePopup.onClickBtn = () => {
            this.heroSpaceGo.SetActive(true);
        };

        this.uiShopNoticePopup.onShowUI = () => {
            this.heroSpaceGo.SetActive(false);
        };

        this.uiGameResult.Init();
        this.uiShopNoticePopup.Init();
    }

    public void GetRecordText(int enemy, int gold, string time, int wave)
    {
        this.uiGameResult.SetResultText(enemy, gold, time, wave);
    }

    public void GetHighRecord(int highWave, string highTime)
    {
        this.uiGameResult.SetHighRecord(highWave, highTime);
        this.uiGameResult.ShowNewWaveIcon();
    }

    public void ShowPopup()
    {
        this.uiShopNoticePopup.ShowUI();
    }
}
