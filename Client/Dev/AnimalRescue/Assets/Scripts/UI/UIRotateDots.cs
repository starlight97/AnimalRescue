using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRotateDots : MonoBehaviour
{
    public Image[] arrChildImg;

    public void Init()
    {
        Hide();
    }

    public void Show()
    {
        Debug.Log("dots ");
        foreach (var img in arrChildImg)
        {
            img.gameObject.SetActive(true);
        }
    }

    public void Hide()
    {
        foreach (var img in arrChildImg)
        {
            img.gameObject.SetActive(false);
        }
    }
}
