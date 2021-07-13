using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    Monster monster;

    public void Awake() {
        monster = GetComponent<Monster>();
    }
}
