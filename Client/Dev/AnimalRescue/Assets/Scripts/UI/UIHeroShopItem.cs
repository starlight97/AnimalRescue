using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroShopItem : MonoBehaviour
{
    private int sp;
    public int id;
    public int price;
    public Button btnPurchase;
    public Image imgHero;

    public Text textHeroName;
    public Text textPirce;
    public void Init(int id, string heroName, int price, Sprite sprite)
    {
        this.id = id;
        this.textHeroName.text = heroName;
        this.price = price;
        this.textPirce.text = this.price.ToString();
        this.imgHero.sprite = sprite;
    }
}
