using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIHeroListItem : MonoBehaviour
{
    public AudioSource audioSource;
    public Button btnHeroListItem;
    public Image imgHero;
    public Text textHeroName;
    public int id;
    public void Init(int id, Sprite sp, string heroName)
    {
        this.audioSource = GetComponent<AudioSource>();
        this.btnHeroListItem = GetComponent<Button>();
        this.imgHero.sprite = sp;
        this.id = id;

        this.textHeroName.text = heroName;
    }

}
