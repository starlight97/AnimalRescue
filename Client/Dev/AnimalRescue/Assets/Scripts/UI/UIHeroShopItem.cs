using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroShopItem : MonoBehaviour
{
    private int sp;
    public Button btnPurchase;
    public Image imgHero;

    public Text textHeroName;
    public Text textPirce;
    public void Init(string heroName, int price)
    {
        this.textHeroName.text = heroName;
        this.textPirce.text = price.ToString();
    }
}
