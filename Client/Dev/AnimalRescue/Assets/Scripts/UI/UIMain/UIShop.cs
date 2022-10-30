using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop : MonoBehaviour
{
    private UIHeroShop uiHeroShop;

    public void Init()
    {
        this.uiHeroShop = GameObject.FindObjectOfType<UIHeroShop>();

        this.uiHeroShop.Init(0);
    }
}
