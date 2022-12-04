using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIAboutScrollRect : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    public GameObject textToScroll;
    private Vector3 startPosition;

    public void Init()
    {
        this.startPosition = textToScroll.transform.position;
    }

    public void ScrollText()
    {
        StartCoroutine(this.ScrollTextRoutine());
    }

    private IEnumerator ScrollTextRoutine()
    {
        textToScroll.transform.position = startPosition;

        var viewRectTrans = this.GetComponent<RectTransform>();
        var textRectTrans = this.textToScroll.GetComponent<RectTransform>();
        
        while (true)
        {
            bool checkOverlap = rectOverlaps(textRectTrans, viewRectTrans);

            if (checkOverlap)
                textToScroll.transform.Translate(0, scrollSpeed * Time.deltaTime, 0);
            else
                textToScroll.transform.position = startPosition;
            yield return null;
        }
    }

    public void ResetStartPos()
    {
        StopAllCoroutines();
    }

    private bool rectOverlaps(RectTransform textRT, RectTransform viewRT)
    {
        if (!textRT || !viewRT) return false;

        var tPosX = textRT.localPosition.x;
        var tPosY = textRT.localPosition.y;
        var vPosX = viewRT.localPosition.x;
        var vPosY = viewRT.localPosition.y;

        var textRect = new Rect(tPosX, tPosY, textRT.rect.width, textRT.rect.height);
        var viewRect = new Rect(vPosX, vPosY, viewRT.rect.width, viewRT.rect.height * 1.8f);

        return textRect.Overlaps(viewRect);
    }
}
