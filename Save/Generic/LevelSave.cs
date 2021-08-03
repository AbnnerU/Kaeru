using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSave : MonoBehaviour
{
    [SerializeField] private EndLevel endLevel;

    private void Awake()
    {
        endLevel.OnLevelEnd += EndLevel_OnlevelEnd;
    }

    private void EndLevel_OnlevelEnd(string name)
    {
        LevelName level = new LevelName();

        level.levelName = name;

        Save.SaveLevelName(level);
    }
}

[System.Serializable]
public class LevelName
{
    public string levelName;
}
