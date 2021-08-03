using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchActions :MonoBehaviour, IOnSwitchActions
{
    [SerializeField] private InputEventArea interactionArea;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private AudioListener audioListener;

    [SerializeField] private float delayToMove;

    public void OnSelectedAction()
    {
        interactionArea.enabled = true;

        audioListener.enabled = true;

        StartCoroutine(EnableMovement());

    }

    public void OnDeselectedAction()
    {
        StopAllCoroutines();

        audioListener.enabled = false;

        interactionArea.enabled = false;

        playerMovement.enabled = false;

    }    

    IEnumerator EnableMovement()
    {
        yield return new WaitForSeconds(delayToMove);

        playerMovement.enabled = true;
    }
}
