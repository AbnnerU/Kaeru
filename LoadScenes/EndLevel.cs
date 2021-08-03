using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevel : MonoBehaviour
{

    [SerializeField] private string levelToLoad;

    public Action<string> OnLevelEnd; 

    private ScenesLoadManeger scenesLoadManeger;

    // Start is called before the first frame update
    void Start()
    {
        scenesLoadManeger = FindObjectOfType<ScenesLoadManeger>();
        //print(scenesLoadManeger);
    }

    private void OnTriggerEnter(Collider other)
    {
        print("ola");
        GrabAndRelease grabAndRelease = other.gameObject.GetComponentInChildren<GrabAndRelease>();
        if (grabAndRelease != null)
        {
            print(grabAndRelease.GetGrabMainLight());
            if (grabAndRelease.GetGrabMainLight() == true)
            {
                OnLevelEnd?.Invoke(levelToLoad);

                LoadScene();
            }
        }
    }

    public void LoadScene()
    {
        scenesLoadManeger.SetLevelToLoad(levelToLoad);
        Save.DeletePositionsData();
        SceneManager.LoadScene("LoadingScene");
    }

    public void CallEvent()
    {
        OnLevelEnd?.Invoke(levelToLoad);
    }
}
