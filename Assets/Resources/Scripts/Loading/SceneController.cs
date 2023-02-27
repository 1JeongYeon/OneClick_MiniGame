using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    private static SceneController sceneController;

    public static SceneController Instance
    {
        get
        {
            if (sceneController == null) sceneController = FindObjectOfType<SceneController>();
            return sceneController;
        }
    }

    public string sceneName = "Title";
    public Slider slider;
    private AsyncOperation operation;

    void Start()
    {
        LoadSceneActive(sceneName);
    }

    public void LoadSceneActive(string _sceneName)
    {
        _sceneName = sceneName;
        StartCoroutine(LoadCoroutine(sceneName));
    }

    IEnumerator LoadCoroutine(string _Scenename)
    {
        sceneName = _Scenename;
        operation = SceneManager.LoadSceneAsync(sceneName);
        operation.allowSceneActivation = false;

        float timer = 0f;
        while (!operation.isDone)
        {
            yield return null;

            timer += Time.deltaTime;
            if (operation.progress < 0.9f)
            {
                slider.value = Mathf.Lerp(operation.progress, 1f, timer);
                if (slider.value >= operation.progress)
                    timer = 0f;
            }
            else
            {
                slider.value = Mathf.Lerp(slider.value, 1f, timer);
                if (slider.value >= 0.99f)
                {
                    operation.allowSceneActivation = true;
                }
            }
        }
    }
    public void OpenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
