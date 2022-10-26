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
    public Text textHeroName;
    public void Init(int heroId)
    {
        this.btnBack.onClick.AddListener(() =>
        {
            this.onClickLobby();
        });

        var heroData = DataManager.instance.GetData<HeroData>(heroId);
        
        this.textHeroName.text = heroData.heroname;
    }
}
