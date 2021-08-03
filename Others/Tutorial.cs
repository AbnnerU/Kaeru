using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Tutorial : MonoBehaviour
{
    [SerializeField] private InputController inputController;

    [SerializeField] private GameObject tutorialText;

    [SerializeField] private GameObject wasd;

    [SerializeField] private GameObject space;

    [SerializeField] private GameObject spaceG;

    [SerializeField] private GameObject ctrl;

    private bool alreadyShow=false;

    private bool alreadyShow1 = false;

    private bool alreadyShow2 = false;

    private void Awake()
    {
        if (Save.LoadObjectsPosition() != null)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
        else
        {
            inputController.OnSwitchEvent += InputController_OnSwitch;

            inputController.OnFlyEvent += InputController_OnFly;

            inputController.OnDonwEvent += InputController_OnDown;
        }        
    }

    private void InputController_OnSwitch()
    {
        if (alreadyShow == false)
        {
            alreadyShow = true;
            tutorialText.SetActive(false);

            spaceG.SetActive(true);

            ctrl.SetActive(true);

            StartCoroutine(Delay());
        }
    }

    private void InputController_OnFly(float value)
    {
        if (alreadyShow && alreadyShow1==false)
        {
            alreadyShow1 = true;
           spaceG.SetActive(false);
        }
    }

    private void InputController_OnDown(float value)
    {
        if (alreadyShow && alreadyShow2==false)
        {
            alreadyShow2 = true;

            ctrl.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && alreadyShow == false)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;

            wasd.SetActive(false);

            space.SetActive(false);

            tutorialText.SetActive(true);
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(5);

        alreadyShow1 = true;

        alreadyShow2 = true;

        ctrl.SetActive(false);

        spaceG.SetActive(false);

        StopAllCoroutines();
    }
}
