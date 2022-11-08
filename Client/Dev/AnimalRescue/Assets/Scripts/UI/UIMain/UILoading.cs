using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoading : UIBase
{
    public Image imgSliderFront;
    public Text textDataName;
    public Text textPer;
    public Image imgPupu;

    public override void Init()
    {
        //this.imgSliderFront = transform.Find("Slider").Find("Front").GetComponent<Image>();
        //this.textDataName = transform.Find("TextDataName").GetComponent<Text>();
        //this.textPer = transform.Find("TextPer").GetComponent<Text>();
    }

    public void SetUI(string dataName, float progress)
    {
        this.textPer.text = string.Format("{0}%", (int)progress * 100f);
        this.textDataName.text = dataName;
        this.imgSliderFront.fillAmount = progress;
        this.imgPupu.fillAmount = progress;

        if (this.imgPupu.fillAmount >= 0.6f)
            this.imgPupu.fillAmount = 0.6f;
    }

}
