using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHeroDetailStats : MonoBehaviour
{
    private int heroId;
    private Text textHeroName;
    private Text textDamage;
    private Text textDamageLevel;
    private Text textMaxHp;
    private Text textMaxHpLevel;
    private Text textMoveSpeed;
    private Text textMoveSpeedLevel;

    private HeroData heroData;
    public void Init(int heroId)
    {
        this.textHeroName = transform.Find("TextHeroName").GetComponent<Text>();
        this.textDamageLevel = transform.Find("TextDamageLevel").GetComponent<Text>();
        this.textDamage = transform.Find("TextDamage").GetComponent<Text>();
        this.textMaxHpLevel = transform.Find("TextMaxHpLevel").GetComponent<Text>();
        this.textMaxHp = transform.Find("TextMaxHp").GetComponent<Text>();
        this.textMoveSpeedLevel = transform.Find("TextMoveSpeedLevel").GetComponent<Text>();
        this.textMoveSpeed = transform.Find("TextMoveSpeed").GetComponent<Text>();

        this.heroId = heroId;
        heroData = DataManager.instance.GetData<HeroData>(heroId);
        this.textHeroName.text = heroData.hero_name;
    }

    public void UpdateUI()
    {
        var info = InfoManager.instance.GetInfo();

        this.textDamageLevel.text = "Level : " + info.dicHeroInfo[heroId].dicStats["damage"].ToString();
        this.textDamage.text = "Damage : " + (heroData.damage + (info.dicHeroInfo[heroId].dicStats["damage"] * heroData.increase_damage)).ToString();
        this.textMaxHpLevel.text = "Level : " + info.dicHeroInfo[heroId].dicStats["maxhp"].ToString();
        this.textMaxHp.text = "Hp : " + (heroData.max_hp + (info.dicHeroInfo[heroId].dicStats["maxhp"] * heroData.increase_maxhp)).ToString();
        this.textMoveSpeedLevel.text = "Level : " + info.dicHeroInfo[heroId].dicStats["movespeed"].ToString();
        this.textMoveSpeed.text = "MoveSpeed : " + (heroData.move_speed + (info.dicHeroInfo[heroId].dicStats["movespeed"] * heroData.increase_movespeed)).ToString();
    }
}
