using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private PlayerMovement playerMovement;

    [SerializeField] private GrabAndRelease grabAndRelease;

    private void Awake()
    {
        playerMovement.OnMoveEvent += PlayerMovement_OnMove;

        playerMovement.OnGroundEvent += PlayerMovement_OnGround;

        playerMovement.OnFallEvent += PlayerMovement_OnFall;

        playerMovement.OnJumpEvent += PlayerMovement_OnJump;

        grabAndRelease.OnGrabing += GrabAndRelease_OnGrabing;

    }

    private void GrabAndRelease_OnGrabing(bool holding)
    {
        anim.SetBool("Holding", holding);
    }

    private void PlayerMovement_OnMove(float value)
    {
        anim.SetFloat("Movement Value", value);
    }

    private void PlayerMovement_OnGround(bool onGround)
    {
        anim.SetBool("OnGround", onGround);
    }

    private void PlayerMovement_OnFall(bool fallingDown)
    {
        anim.SetBool("Falling", fallingDown);
    }

    private void PlayerMovement_OnJump(bool jumping)
    {
        anim.SetBool("Jumping", jumping);
    }
}
