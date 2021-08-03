using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CanvasManeger_MainMenu : MonoBehaviour
{
    [SerializeField] private Button newGameButton;

    [SerializeField] private Button continueButton;

    [SerializeField] private GameObject confirmScreen;

    private ScenesLoadManeger scenesLoadManeger;

    private string confirmLevel;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;

        Time.timeScale = 1;

        if (Save.LoadLevel() == null )
        {
            continueButton.interactable = false;
        }
        else
        {
            continueButton.interactable = true;
        }
    }

    void Start()
    {
        scenesLoadManeger = FindObjectOfType<ScenesLoadManeger>();
    }

    #region New Game
    public void NewGame(string firtLevelName)
    {
        if(Save.LoadLevel() != null)
        {
            confirmScreen.SetActive(true);

            confirmLevel = firtLevelName;
        }
        else
        {
            scenesLoadManeger.SetLevelToLoad("Animação");

            LevelName name = new LevelName();

            name.levelName = firtLevelName;

            Save.DeletePositionsData();

            Save.SaveLevelName(name);        

            SceneManager.LoadScene("LoadingScene");
        }
 
    }

    public void Confirm()
    {
        scenesLoadManeger.SetLevelToLoad("Animação");

        LevelName name = new LevelName();

        name.levelName = confirmLevel;

        Save.DeletePositionsData();

        Save.SaveLevelName(name);

        SceneManager.LoadScene("LoadingScene");
    }


    public void Exit()
    {
        Application.Quit();
    }

    

    public void CloseConfirmScreen()
    {
        confirmScreen.SetActive(false);
    }
    #endregion

    public void ContinueGame()
    {
        scenesLoadManeger.SetLevelToLoad(Save.LoadLevel().levelName);

        SceneManager.LoadScene("LoadingScene");
    }

    public void DeleteData()
    {
        Save.DeleteAllData();

        ConfigsSave.DeleteAll();
    }
}
