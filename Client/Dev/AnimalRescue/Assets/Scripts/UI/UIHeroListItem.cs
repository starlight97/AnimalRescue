using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIHeroListItem : MonoBehaviour
{
    public Text textHeroName;
    public int id;
    public void Init(int id, string heroName)
    {
        this.id = id;

        this.textHeroName.text = heroName;
    }
}
