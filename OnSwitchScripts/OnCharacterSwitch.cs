using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCharacterSwitch : MonoBehaviour
{
    [SerializeField] private SwitchCharacter switchCharacter;

    [SerializeField] private GameObject[] characters;


    private void Start()
    {
        switchCharacter.OnCharacterSelected += SwitchCharacter_OnCharacterSelected;

        switchCharacter.OnCharacterDeselect += SwitchCharacter_OnCharacterDeselected;
    }

    private void SwitchCharacter_OnCharacterSelected(int characterID)
    {
        //Selected Action   
        characters[characterID].GetComponent<IOnSwitchActions>()?.OnSelectedAction();
        //print(characterID);

    }

    private void SwitchCharacter_OnCharacterDeselected(int characterID)
    {
        //Deselected Action   
        characters[characterID].GetComponent<IOnSwitchActions>()?.OnDeselectedAction();
    }
}
