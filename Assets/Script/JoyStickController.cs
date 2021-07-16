using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoyStickController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // ĳ���� �̵� �� ���� ��Ʈ�ѷ�

    Character_Move c_move;
    Character_Weapon c_weapon;

    [SerializeField] GameObject Player;

    [SerializeField] private RectTransform lever;
    private RectTransform rectTransform;

    [SerializeField, Range(0, 100)] private float leverRange;

    private Vector2 inputDir;
    private bool is_L_Input, is_R_Input;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        c_move = Player.GetComponent<Character_Move>();
        c_weapon = Player.GetComponent<Character_Weapon>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);

        if (rectTransform.position.x < 100)
            is_L_Input = true;
        else
            is_R_Input = true;

    }

    public void OnDrag(PointerEventData eventData)
    {
        ControlJoystickLever(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        lever.anchoredPosition = Vector2.zero;

        if (rectTransform.position.x < 100)
            is_L_Input = false;
        else
            is_R_Input = false;
    }

    private void ControlJoystickLever(PointerEventData eventData)
    {
        var inputPos = rectTransform.position.x > 0 ? eventData.position - rectTransform.anchoredPosition : eventData.position;
        var inputVector = inputPos.magnitude < leverRange ? inputPos : inputPos.normalized * leverRange;
        lever.anchoredPosition = inputVector;
        inputDir = inputVector / leverRange;
    }

    void Update()
    {
        if (is_L_Input)
            c_move.Move(inputDir);

        if (is_R_Input)
            c_weapon.Weapon_Aiming(inputDir);
        else //��� ������ �ż� ���� �� �����Դϴ�!
            c_weapon.Weapon_Shot(inputDir);
    }
}