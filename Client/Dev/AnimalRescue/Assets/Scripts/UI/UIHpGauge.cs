using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpGauge : MonoBehaviour
{
    private RectTransform rectTrans;
    public RectTransform canvasRectTrans;
    private Image imgFill;

    private void Awake()
    {
        this.rectTrans = GetComponent<RectTransform>();
        this.imgFill = GameObject.Find("fill").GetComponent<Image>();
    }

    public void UpdatePosition(Vector3 worldPos)
    {
        Vector3 screenPosition = RectTransformUtility.WorldToScreenPoint(Camera.main, worldPos);
        this.rectTrans.position = screenPosition;
    }

    public void DecreaseHp(int hp, int maxHp)
    {
       this.imgFill.fillAmount = (float)hp / (float)maxHp;
    }
}
