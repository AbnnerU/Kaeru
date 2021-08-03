using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasAim : MonoBehaviour
{
    private CanvasManeger_Game canvasManeger;

    private InteractionRaycast interactionRaycast;

    private void Awake()
    {
        canvasManeger = FindObjectOfType<CanvasManeger_Game>();

        interactionRaycast = GetComponent<InteractionRaycast>();

        interactionRaycast.OnEnableInteraction += InteractionRaycast_OnEnableInteraction;

        interactionRaycast.OnTouchObject += InteractionRaycat_OnTouchObject;

        //interactionRaycast.OnInteracting += InteractionRaycast_OnInteracting;

    }


    public void InteractionRaycast_OnEnableInteraction(bool enabled)
    {
        //print("Mirando");
        canvasManeger.ShowGhostAim(enabled);
    }

    public void InteractionRaycat_OnTouchObject(bool touching)
    {
        //print("Objeto");
        canvasManeger.AimAnimation(touching);
    }

    public void InteractionRaycast_OnInteracting(bool interacting)
    {
        //print("Interagindo");
        canvasManeger.ShootAnimation(interacting);
    }
}
