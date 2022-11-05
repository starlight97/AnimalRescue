using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIBase
{
    private Image imgSliderFront;
    private Text textDataName;
    private Text textPer;

    public override void Init()
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
