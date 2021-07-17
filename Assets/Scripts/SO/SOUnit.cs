using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOUnit", menuName = "SO/Unit", order = 0)]
public class SOUnit : ScriptableObject
{
    [SerializeField] Sprite minimapIcon;
    public Sprite MinimapIcon => minimapIcon;
<<<<<<< HEAD
    [SerializeField] float iconSize;
    public float IconSize => iconSize;
=======
>>>>>>> origin/main
    [SerializeField] int maxHp;
    public int MaxHP => maxHp;
    [SerializeField] float speed;
    public float Speed => speed;
    [SerializeField] float rotateSpeed;
    public float RotateSpeed => rotateSpeed;
    [SerializeField] float attackRange;
    public float AttackRange => attackRange;
<<<<<<< HEAD
}
=======
}
>>>>>>> origin/main
