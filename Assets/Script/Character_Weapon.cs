using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Weapon : MonoBehaviour
{
    //��Ʈ�ѷ����� ���� ���� ������ ���� ���� ���� �� �߻� ���
    [SerializeField] private Weapon_Unit WeaponData;
    [SerializeField] private Image ArrowImage;//���� ���� UI

    public void Weapon_Aiming(Vector2 inputDir) //����
    {
        Vector3 weaponDir = new Vector3(inputDir.x, inputDir.y, 0f);
        float angle = Quaternion.FromToRotation(Vector3.up, weaponDir).eulerAngles.z; //���� ���ϱ�

        ArrowImage.rectTransform.rotation = Quaternion.Euler(90f, 0f, angle);
        ArrowImage.rectTransform.localScale = (new Vector3(1f, 1f, 1f));
    }
   
    public void Weapon_Shot(Vector2 inputDir) //�߻� (���� �ۼ� ���߽��ϴ�!!)
    {
        ArrowImage.rectTransform.localScale = (new Vector3(1f, 0f, 1f));

        //Vector3 shotDir = new Vector3(inputDir.x, inputDir.y, 0f);
    }
}

