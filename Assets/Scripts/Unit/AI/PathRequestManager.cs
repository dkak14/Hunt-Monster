using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class PathRequest {
    public Vector3 pathStart;
    public Vector3 pathEnd;
    public PathRequest(Vector3 pathStart, Vector3 pathEnd) {
        this.pathStart = pathStart;
        this.pathEnd = pathEnd;
    }
}
