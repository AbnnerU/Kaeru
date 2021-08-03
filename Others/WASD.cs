
using UnityEngine;

public class WASD : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    [SerializeField] private GameObject wasd;

    [SerializeField] private GameObject space;

    //[SerializeField] private GameObject spaceG;

    //[SerializeField] private GameObject ctrl;

    private bool alreadyShow = false;

    private bool alreadyShow2 = false;

    private void Awake()
    {
        if (Save.LoadObjectsPosition() != null)
        {
            wasd.SetActive(false);
        }
        else
        {
            inputController.OnMoveEvent += InputController_OnMove;

            inputController.OnJumpEvent += InputController_OnJump;

            wasd.SetActive(true);
        }
    }

    private void InputController_OnMove(Vector2 values)
    {
        if (alreadyShow == false)
        {
            alreadyShow = true;
            wasd.SetActive(false);

            space.SetActive(true);
        }
    }

    private void InputController_OnJump(float value)
    {
        if (alreadyShow2 == false && alreadyShow==true)
        {
            alreadyShow2 = true;
            space.SetActive(false);
        }
    }
}
