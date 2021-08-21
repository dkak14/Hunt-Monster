using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    public CanvasGroup fadeCg;
    [SerializeField] Image fadeImage;

    [Range(0.5f, 2.0f)]
    public float fadeDuration = 1.0f;

    public Dictionary<string, LoadSceneMode> loadScenes = new Dictionary<string, LoadSceneMode>();
    
    void InitSceneInfo()
    {
        loadScenes.Add("TestRoom0", LoadSceneMode.Additive);
        //loadScenes.Add("MainScene", LoadSceneMode.Additive);
    }

    IEnumerator Start()
    {
        InitSceneInfo();

        fadeCg.alpha = 1.0f;
        
        foreach(var _loadScene in loadScenes)
        {
            yield return StartCoroutine(LoadScene(_loadScene.Key, _loadScene.Value));
        }

        StartCoroutine(Fade(0.0f));
    }

    IEnumerator LoadScene(string sceneName, LoadSceneMode mode)
    {
        //비동기 씬 로드
        yield return SceneManager.LoadSceneAsync(sceneName, mode);

        Scene loadedScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(loadedScene);
    }

    IEnumerator Fade(float finalAlpha)
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("TestRoom0"));
        fadeCg.blocksRaycasts = true;

        while (fadeImage.color.a > 0)
        {
            fadeImage.DOFade(0, 1);
            yield return null;
        }

        fadeCg.blocksRaycasts = false;

        SceneManager.UnloadSceneAsync("SceneLoader");
    }

}
