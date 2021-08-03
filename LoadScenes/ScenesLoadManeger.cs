using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenesLoadManeger : MonoBehaviour
{
    [SerializeField] private string currentLevelToLoad;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void SetLevelToLoad(string name)
    {
        currentLevelToLoad = name;
    }

    public string GetLevelToLoad()
    {
        return currentLevelToLoad;
    }
}
