using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyHeroStats : MonoBehaviour
{
    private Text textHeroName;
    private Text textDamage;
    private Text textHp;
    private Text textMoveSpeed;

    public void Init()
    {
        this.textHeroName = transform.Find("TextHeroName").GetComponent<Text>();
        this.textDamage = transform.Find("TextDamage").GetComponent<Text>();
        this.textHp = transform.Find("TextHp").GetComponent<Text>();
        this.textMoveSpeed = transform.Find("TextMoveSpeed").GetComponent<Text>();
    }

    public void UIUpdate(string heroName, int damage, int hp, int moveSpeed)
    {
        this.textHeroName.text = heroName;
        this.textDamage.text = "DAMAGE : " + damage.ToString();
        this.textHp.text = "HP : " + hp.ToString();
        this.textMoveSpeed.text = "MOVESPEED : " + moveSpeed.ToString();
    }
}
