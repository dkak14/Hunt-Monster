using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TupleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        (double, int) t1 = (4.5f, 1);
        Debug.Log(t1.Item1 + "  " + t1.Item2);

    }

}
