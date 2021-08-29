using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class NormalFade : FadeInOut
{
    public override void FadeIn(Image fadeImage, float duration, Action callback) {
        Color fadeImageColor = fadeImage.color;
        fadeImage.color = new Color(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b, 1);
        fadeImage.DOFade(0, duration).OnComplete(()=> { if(callback != null)callback(); });
    }

    public override void FadeOut(Image fadeImage, float duration, Action callback) {
        Color fadeImageColor = fadeImage.color;
        fadeImage.color = new Color(fadeImageColor.r, fadeImageColor.g, fadeImageColor.b, 0);
        fadeImage.DOFade(1, duration).OnComplete(() => { if (callback != null) callback(); });
    }

}
