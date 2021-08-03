using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class SwitchCharacter : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook characterOneCamera;

    [SerializeField] private CinemachineFreeLook characterTwoCamera;

    [SerializeField] private bool disableCameraWhenSwitching=true;

    public Action<int> OnCharacterSelected;

    public Action<int> OnCharacterDeselect;

    //[Header("Characters")]
    //[SerializeField] private Transform[] charactersCameraFollow;
    private enum CharacterNumber { CharacterOne, CharacterTwo }

    private CharacterNumber currentCharacter;

    private InputController inputController;

    //private bool canSwitch;

    private void Start()
    {
        inputController = FindObjectOfType<InputController>();

        inputController.OnSwitchEvent += InputController_OnSwitchEvent;

        currentCharacter = CharacterNumber.CharacterOne;

        characterOneCamera.enabled = true;
        characterTwoCamera.enabled = false;
    }


    private void InputController_OnSwitchEvent()
    {                
         ChangeCharacter();    
    }

    public void ChangeCharacter()
    {
        if (currentCharacter == CharacterNumber.CharacterOne)
        {
            currentCharacter = CharacterNumber.CharacterTwo;

            //Enable
            characterTwoCamera.enabled = true;

            characterOneCamera.Priority = 0;
            characterTwoCamera.Priority = 1;

            //Disable
            characterOneCamera.enabled = !disableCameraWhenSwitching;

            //Event
            OnCharacterDeselect?.Invoke(0);
            OnCharacterSelected?.Invoke(1);

        }
        else
        {
            currentCharacter = CharacterNumber.CharacterOne;

            //Enable
            characterOneCamera.enabled = true;

            characterOneCamera.Priority = 1;
            characterTwoCamera.Priority = 0;

            //Disable
            characterTwoCamera.enabled = !disableCameraWhenSwitching;

            //Event
            OnCharacterDeselect?.Invoke(1);
            OnCharacterSelected?.Invoke(0);
        }

        //print(currentCharacter);
    }

    //public void SetCanSwitch(bool value)
    //{
    //    canSwitch = value;
    //}
}
