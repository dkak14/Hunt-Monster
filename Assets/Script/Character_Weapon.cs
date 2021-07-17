using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character_Weapon : MonoBehaviour
{
    //컨트롤러에서 받은 방향 값으로 조준 방향 설정 및 발사 담당
    [SerializeField] private Weapon_Unit WeaponData;
    [SerializeField] private Image ArrowImage;//조준 방향 UI

    public void Weapon_Aiming(Vector2 inputDir) //조준
    {
        Vector3 weaponDir = new Vector3(inputDir.x, inputDir.y, 0f);
        float angle = Quaternion.FromToRotation(Vector3.up, weaponDir).eulerAngles.z; //각도 구하기

        ArrowImage.rectTransform.rotation = Quaternion.Euler(90f, 0f, angle);
        ArrowImage.rectTransform.localScale = (new Vector3(1f, 1f, 1f));
    }
   
    public void Weapon_Shot(Vector2 inputDir) //발사 (아직 작성 안했습니다!!)
    {
        ArrowImage.rectTransform.localScale = (new Vector3(1f, 0f, 1f));

        //Vector3 shotDir = new Vector3(inputDir.x, inputDir.y, 0f);
    }
}

