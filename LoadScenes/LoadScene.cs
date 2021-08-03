using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] private string levelName;

    [SerializeField] private bool loadDifined=false;

    private void Start()
    {
        if (loadDifined)
        {
            SceneManager.LoadScene(levelName);
        }
        else
        {
            //print("Ola");
            ScenesLoadManeger scenesLoadManeger = FindObjectOfType<ScenesLoadManeger>();

            levelName = scenesLoadManeger.GetLevelToLoad();

            StartCoroutine(LoadLevelAsync());
        }
    }

    public void SetLevelName(string name)
    {
        levelName = name;
    }

    IEnumerator LoadLevelAsync()
    {
        //print(levelName);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            yield return null;
        }
    }
}
