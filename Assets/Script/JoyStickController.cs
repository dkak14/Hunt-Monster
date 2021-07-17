using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] SOJoyStickValue JoyStickValue;
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(0, 100)] private float leverRange;

    private Vector2 inputDir;
    private bool is_L_Input, is_R_Input;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        JoyStickValue.Playing = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        JoyStickValue.Playing = false;
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = rectTransform.position.x > 0 ? eventData.position - rectTransform.anchoredPosition : eventData.position;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDir = inputVector / leverRange;
        JoyStickValue.Value = inputDir;
    }
}