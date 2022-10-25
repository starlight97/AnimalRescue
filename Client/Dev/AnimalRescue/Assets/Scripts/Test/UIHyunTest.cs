using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHyunTest : MonoBehaviour
{
    private UIHpGauge uiHpGauge;

    public void Init()
    {
        this.uiHpGauge = this.transform.Find("UIHpGauge").GetComponent<UIHpGauge>();
        this.uiHpGauge.Init();
    }

    public void UpdatePosition(Vector3 worldPos)
    {
        this.uiHpGauge.UpdatePosition(worldPos);
    }

    public void UpdateUIHpGauge(float hp, float maxHp)
    {
        this.uiHpGauge.UpdateUI(hp, maxHp);
    }
}
