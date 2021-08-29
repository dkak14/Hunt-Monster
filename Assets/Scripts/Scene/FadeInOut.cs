using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
public abstract class FadeInOut : MonoBehaviour
{
    public abstract void FadeIn(Image fadeImage ,float duration, Action callback);
    public abstract void FadeOut(Image fadeImage,float duration, Action callback);
}
