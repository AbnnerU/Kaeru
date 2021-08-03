
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SkipAnimation : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    [SerializeField] private VideoPlayer video;

    [SerializeField] private string nextLevel;

    private bool skiped = false;

    private ScenesLoadManeger scenesLoadManeger;

    // Start is called before the first frame update
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        video.loopPointReached += Video_loopPointReached;

        inputController.OnInteractEvent += InputController_OnInteract;
    }

    private void Video_loopPointReached(VideoPlayer source)
    {
        if (skiped == false)
        {
            skiped = true;
            scenesLoadManeger.SetLevelToLoad(nextLevel);
            SceneManager.LoadScene("LoadingScene");
            Destroy(gameObject);
        }
    }

    void Start()
    {
        scenesLoadManeger = FindObjectOfType<ScenesLoadManeger>();
    }

    private void InputController_OnInteract()
    {
        if (skiped == false)
        {
            skiped = true;
            scenesLoadManeger.SetLevelToLoad(nextLevel);
            SceneManager.LoadScene("LoadingScene");
            Destroy(gameObject);
        }

    }

    
}
