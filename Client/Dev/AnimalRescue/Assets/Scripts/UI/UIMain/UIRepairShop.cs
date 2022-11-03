using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIRepairShop : MonoBehaviour
{
    public Button btnBack;
    public UnityAction onClickLobby;
    public Text textPlayerGold;
    public Text textPlayerDiamond;
    public GameObject heroViewGo;
    public AudioSource audioSource;
    private int powerUpConut = 0;

    private UIPowerUpStat uiPowerUpStat;
    private UIHeroDetailStats uiHeroDetailStats;

    public ParticleSystem heartsParticleGo;
    public ParticleSystem starParticleGo;
    public void Init(int heroId)
    {
        var info = InfoManager.instance.GetInfo();
        this.textPlayerGold.text = info.playerInfo.gold.ToString();
        this.textPlayerDiamond.text = info.playerInfo.diamond.ToString();

        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });

        this.uiPowerUpStat = GameObject.FindObjectOfType<UIPowerUpStat>();
        this.uiHeroDetailStats = GameObject.FindObjectOfType<UIHeroDetailStats>();
        this.uiPowerUpStat.Init(heroId);
        this.uiHeroDetailStats.Init(heroId);

        var heroData = DataManager.instance.GetData<HeroData>(heroId);
        var uiHeroGo = Instantiate(Resources.Load<GameObject>(heroData.ui_prefab_path), heroViewGo.transform);
        var uiHero = uiHeroGo.GetComponent<UIHero>();
        uiHero.Init();

        uiPowerUpStat.onClickLevelUp = (statType) =>
        {
            audioSource.Play();
            this.uiHeroDetailStats.UpdateUI();
            this.textPlayerGold.text = info.playerInfo.gold.ToString();
            this.textPlayerDiamond.text = info.playerInfo.diamond.ToString();
            powerUpConut++;
            if(powerUpConut % 3 == 0)
            {
                uiHero.SetAnim(UIHero.eState.PowerUp02);
                heartsParticleGo.Play();
                //PlayParticle(2);
            }
            else
            {
                uiHero.SetAnim(UIHero.eState.PowerUp01);
                starParticleGo.Play();
                //PlayParticle(1);
            }

        };
        this.uiHeroDetailStats.UpdateUI();
    }

    // 1 별
    // 2 하트
    private Coroutine playParticleRoutine;
    //private void PlayParticle(int type)
    //{
    //    if (playParticleRoutine != null)
    //        StopCoroutine(playParticleRoutine);
    //    playParticleRoutine = this.StartCoroutine(this.PlayParticleRoutine(type));
    //}
    //private IEnumerator PlayParticleRoutine(int type)
    //{
    //    //if(type == 1)
    //    //{
    //    //    starParticleGo.SetActive(true);
    //    //}
    //    //else
    //    //{
    //    //    heartsParticleGo.SetActive(true);
    //    //}
    //    //yield return new WaitForSeconds(3f);
    //    //starParticleGo.SetActive(false);
    //    //heartsParticleGo.SetActive(false);
    //    //playParticleRoutine = null;
    //}
}
