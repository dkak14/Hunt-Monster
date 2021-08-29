using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Zenject;
public class StartTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimeText;
    [SerializeField] int Time;
    [Inject] IPlaySound PlaySound;
    void Start()
    {
        StartCoroutine(C_StartTime(Time));
    }
    IEnumerator C_StartTime(int time) {
        Vector3 scale = TimeText.rectTransform.localScale;
        while (time > 0) {
            TimeText.text = time.ToString();
            TimeText.rectTransform.localScale = scale * 1.5f;
            TimeText.rectTransform.DOScale(scale.y, 1);
            yield return new WaitForSeconds(1f);
            time--;
        }
        TimeText.text = "Start";
        TimeText.rectTransform.localScale = scale * 1.5f;
        TimeText.rectTransform.DOScale(scale.y, 1).OnComplete(()=>TimeText.gameObject.SetActive(false));
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.TimerEnd, this, null);
    }
}
