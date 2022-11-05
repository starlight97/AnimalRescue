using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIHpGauge : MonoBehaviour
{
    private RectTransform rectTrans;
    private Image imgFill;

    public void Init()
    {
        this.rectTrans = GetComponent<RectTransform>();
        this.imgFill = GameObject.Find("fill").GetComponent<Image>();
        this.imgFill.fillAmount = 1;
    }

    public void UpdatePosition(Vector3 worldPos)
    {
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
        this.rectTrans.position = screenPosition;
    }

    public void UpdateUI(float hp, float maxHp)
    {
        float per = hp / maxHp;
        this.imgFill.fillAmount = per;
    }
}