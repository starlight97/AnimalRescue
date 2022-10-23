using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : MonoBehaviour
{
    private Image imgSliderFront;
    private Text textDataName;
    private Text textPer;

    public void Init()
    {
        this.imgSliderFront = transform.Find("Slider").Find("Front").GetComponent<Image>();
        this.textDataName = transform.Find("TextDataName").GetComponent<Text>();
        this.textPer = transform.Find("TextPer").GetComponent<Text>();
    }

    public void SetUI(string dataName, float progress)
    {
        this.textPer.text = string.Format("{0}%", progress * 100f);
        this.textDataName.text = dataName;
        this.imgSliderFront.fillAmount = progress;
    }

}
