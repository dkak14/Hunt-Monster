using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;
public class StartTimer : MonoBehaviour
{
    [SerializeField] Image TimeImage;
    [SerializeField] Image TimerEndImage;
    [SerializeField] Sprite[] sprites;
    [SerializeField] int Time;
    //[Inject] IPlaySound PlaySound;
    void Start()
    {
        StartCoroutine(C_StartTime(Time));
    }
    IEnumerator C_StartTime(int time) {
        TimeImage.color = new Color(1, 1, 1, 1);
        Vector3 scale = TimeImage.rectTransform.localScale;
        while (time > 0) {
            TimeImage.sprite = sprites[time - 1];
            TimeImage.rectTransform.localScale = scale * 1.5f;
            TimeImage.rectTransform.DOScale(scale.y, 1);
            time--;
            yield return new WaitForSeconds(1f);
        }
        TimeImage.gameObject.SetActive(false);
        TimerEndImage.gameObject.SetActive(true);
        TimerEndImage.rectTransform.localScale = scale;
        TimerEndImage.rectTransform.DOScale(scale.y, 1.001f).OnComplete(() => TimerEndImage.gameObject.SetActive(false)).SetEase(Ease.OutQuint);
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.TimerEnd, this, null);
    }
}
