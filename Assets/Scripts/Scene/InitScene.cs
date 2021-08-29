using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InitScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SceneLoadManager.SceneChange("MainScene", "NormalFadeEffect", 1f);
    }

}
