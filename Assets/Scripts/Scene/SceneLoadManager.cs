using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public static class SceneLoadManager {

    public static void SceneChange(string sceneName, string fadeEffectName, float duration) {
        if (SceneManager.sceneCount < 2) {
            SceneManager.sceneLoaded += (Scene, load) => { EventManager<SceneEvent>.Instance.PostEvent(SceneEvent.SceneChangeStart, null, new object[] { sceneName, fadeEffectName, duration }); };
            Scene nowScene = SceneManager.GetActiveScene();
            SceneManager.LoadSceneAsync("SceneLoader", LoadSceneMode.Additive);
            SceneManager.SetActiveScene(nowScene);
        }
        Debug.Log("sadsad");
    }
}