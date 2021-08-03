using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using TMPro;

public class CanvasManeger_Game : MonoBehaviour
{

    [Header("Pop up inputs text")]
    //[SerializeField] private GameObject inputButtonName;
    [SerializeField] private GameObject homenagem;
    [SerializeField] private GameObject imageButton1;
    [SerializeField] private GameObject imageButton2;
    [SerializeField] private GameObject actionDescription;

    [SerializeField] private GameObject ghostAim;

    [Header("Pause")]

    [SerializeField] private GameObject pausePanel;

    [Header("Timer")]

    [SerializeField] private Slider timerSlider;

    [SerializeField] private GameObject confirmPanel;

    private InputController inputController;

    private ScenesLoadManeger scenesLoadManeger;

    private Animator anim;

    private TMP_Text buttonNameText;
    private TMP_Text actionText;

    private enum GameState{ GameOn,GamePaused }

    private GameState gameState;

    private void Awake()
    {
        inputController = FindObjectOfType<InputController>();
        //buttonNameText = inputButtonName.GetComponent<TMP_Text>();
        actionText = actionDescription.GetComponent<TMP_Text>();
        gameState = GameState.GameOn;

        anim = GetComponent<Animator>();

        inputController.OnPauseEvent += InputController_OnPauseEvent;

        HideInputText();

        ShowGhostAim(false);
    }

    private void Start()
    {
        scenesLoadManeger = FindObjectOfType<ScenesLoadManeger>();

        Pause(false);
    }

    public void InputController_OnPauseEvent()
    {
        if (gameState == GameState.GameOn)
        {
            gameState = GameState.GamePaused;

            Pause(true);
        }
        else
        {
            gameState = GameState.GameOn;

            Pause(false);

        }
    }

    private void Pause(bool pauseGame)
    {
        if (pauseGame)
        {         
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {          
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }

        ShowConfirmPanel(false);

        if (pausePanel != null)
        {
            pausePanel.SetActive(pauseGame);
        }
    }

    public void ToMenu()
    {
        scenesLoadManeger.SetLevelToLoad("Main Menu");
        SceneManager.LoadScene("LoadingScene");
    }
    

    #region InputText
    public void ShowInputText(string buttonName, string actionDescription)
    {
        //inputButtonName.SetActive(true);
        if (buttonName.Contains("[E]"))
        {
            imageButton1.SetActive(true);
            imageButton2.SetActive(false);
        }
        else
        {
            imageButton1.SetActive(false);
            imageButton2.SetActive(true);
        }

        this.actionDescription.SetActive(true);

        //buttonNameText.text = buttonName;
        actionText.text = actionDescription; 
    }

    public void ShowEspecial(string actionDescription)
    {
        homenagem.SetActive(true);
        imageButton1.SetActive(false);
        imageButton2.SetActive(false);
        this.actionDescription.SetActive(true);


        actionText.text = actionDescription;
    }

    public void HideInputText()
    {
        if (actionDescription != null)
        {
            homenagem.SetActive(false);
            imageButton1.SetActive(false);
            imageButton2.SetActive(false);

            //inputButtonName.SetActive(false);
            actionDescription.SetActive(false);
        }
    }
    #endregion

    #region GhostAim
    public void ShowGhostAim(bool active)
    {        
        ghostAim.SetActive(active);
    }

    public void AimAnimation(bool value)
    {
        anim.SetBool("Aim", value);
    }

    public void ShootAnimation(bool value)
    {
        anim.SetBool("Shooting", value);
    }
    #endregion

    #region PanelAnimation/RestartLevel

    public void ShowConfirmPanel(bool active)
    {
        if(confirmPanel!=null)
        confirmPanel.SetActive(active);
    }

    public void Confirm()
    {
        Save.DeletePositionsData();

        ShowConfirmPanel(false);

        StartPanelTrasition();
    }

    public void StartPanelTrasition()
    {
        print("Funciona");
        Pause(false);
        anim.Play("Transition panel", 1,0);
    }

    public void ResetLevel()
    {
        scenesLoadManeger.SetLevelToLoad(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("LoadingScene");
    }

    #endregion

    #region Timer
    public void TimerAnimation(bool active, float time)
    {
        anim.SetBool("Timer", active);

        if (active)
        {
            StartCoroutine(StartTimer(time));
        }
        else
        {
            StopAllCoroutines();
        }
    }

    IEnumerator StartTimer(float totalTimer)
    {
        float currentTime = totalTimer;
        float percentage = 100;

        timerSlider.value = 100;

        while (currentTime > 0)
        {
            print("OLAA");
            currentTime -= Time.deltaTime;

            percentage = (100 * currentTime) / totalTimer;

            timerSlider.value = percentage;

            yield return null;
        }
    }
    
    #endregion

}