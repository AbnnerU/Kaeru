using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputActionsImpediments : MonoBehaviour
{
    [SerializeField] private GameObject mainPlayer;

    [SerializeField] private SwitchCharacter switchCharacter;

    private GrabAndRelease grabScript;

    private PlayerMovement playerMovement;


    private bool playerOnGround;

    private void Awake()
    {

        playerMovement = mainPlayer.GetComponent<PlayerMovement>();
        playerMovement.OnGroundEvent += PlayerMovement_OnGroundEvent;

        grabScript = mainPlayer.GetComponentInChildren<GrabAndRelease>();
      
    }

    private void PlayerMovement_OnGroundEvent(bool grounded)
    {
        playerOnGround = grounded;
    }

    public bool PlayerHoldingObject()
    {
        if (playerMovement.GetMovementActive())
        {
            return grabScript.GetHoldingObject(); 
        }
        else
        {
            return false;
        }
    }

    public bool GetPlayerGrounded()
    {
        if (playerMovement.GetMovementActive())
        {
            return playerOnGround;
        }
        else
        {
            return true;
        }
    }


}
