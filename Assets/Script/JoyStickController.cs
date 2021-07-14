using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(0,100)]
    private float leverRange;

    private Vector2 inputDir;
    private bool isInput;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
        isInput = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;
        isInput = false;
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = eventData.position - rectTransform.anchoredPosition;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDir = inputVector / leverRange;
    }

    private void InputControlVector()
    {
        GameObject.Find("Player").GetComponent<Character_Move>().Move(inputDir);
    }

    void Update()
    {
        if (isInput)
            InputControlVector();
    }
}
