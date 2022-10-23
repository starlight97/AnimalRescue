using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIApp : MonoBehaviour
{
    private Image dim;

    public void Init()
    {
        this.dim = GameObject.Find("dim").GetComponent<Image>();
    }

    public void FadeIn(float duration = 0.5f, TweenCallback callback = null)
    {
        DOTween.ToAlpha(() => this.dim.color, x => this.dim.color = x, 0, duration).SetEase(Ease.InQuad).OnComplete(callback);
    }

    public void FadeOut(float duration = 0.5f, TweenCallback callback = null)
    {
        DOTween.ToAlpha(() => this.dim.color, x => this.dim.color = x, 1, duration).SetEase(Ease.InQuad).OnComplete(callback);
    }

    public void FadeInImmediately()
    {
        var color = this.dim.color;
        color.a = 0;
        this.dim.color = color;
    }

    public void FadeOutImmediately()
    {
        var color = this.dim.color;
        color.a = 1;
        this.dim.color = color;

    }
}
