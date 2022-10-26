using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIHeroListItem : MonoBehaviour
{
    public Button btnHeroListItem;
    public Text textHeroName;
    public int id;
    public void Init(int id, string heroName)
    {
        this.btnHeroListItem = GetComponent<Button>();
        this.id = id;

        this.textHeroName.text = heroName;
    }
}
