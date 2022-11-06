using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UITitle : MonoBehaviour
{
    public Text startText;

    private float delta = 0;

    public void Init()
    {
        blinkText();
    }

    public void blinkText()
    {
        StartCoroutine(blinkTextRoutine());
    }

    private IEnumerator blinkTextRoutine()
    {
        while(true)
        {
            this.startText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            this.startText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
    }
}
