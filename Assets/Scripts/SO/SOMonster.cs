using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOMonster", menuName = "SO/Monster", order = 0)]
public class SOMonster : SOUnit
{
    [SerializeField] int minDropGold;
    public int MinDropGold => minDropGold;
    [SerializeField] int maxDropGold;
    public int MaxDropGold => maxDropGold;
<<<<<<< HEAD
    [SerializeField] int id;
    public int ID => id;
=======
>>>>>>> origin/main
}
