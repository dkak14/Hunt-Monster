using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Weapon_Unit", menuName = "Weapon/Weapon_Unit", order = 0)]

public class Weapon_Unit : ScriptableObject
{
    [SerializeField] string weapon_name;
    public string Weapon_name => weapon_name;

    [SerializeField] int weapon_ID;
    public int Weapon_ID => weapon_ID;

    [SerializeField] float power;
    public float Power => power;

    [SerializeField] float speed; //���� ���߼ӵ� ������ ���� �� ���Ƽ� �ϴ� �־�ý��ϴ� !!
    public float Speed => speed;

    [SerializeField] float reroad_speed;
    public float Reroad_speed => reroad_speed;

    [SerializeField] int bullet_count;
    public int Bullet_cont => bullet_count;
}
