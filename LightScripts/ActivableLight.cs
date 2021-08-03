using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivableLight : InteractiveObjectActions
{
    [SerializeField] private LightState lightState;

    [Header("Config")]
    //[SerializeField] private Collider lightCollider;

    //[SerializeField] private Light theLight;

    [SerializeField] private float actionDuration;

    [SerializeField] private bool startOn = false;

    private Coroutine coroutine;

    private bool alreadyInteracted;
   
    private void OnEnable()
    {
        alreadyInteracted = false;

        lightState.ChangeLightState(startOn);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected override void InteragibleObject_OnInteracted()
    {
        if (alreadyInteracted == false)
        {
            alreadyInteracted = true;

            coroutine = StartCoroutine(LightActionTimer());
        }
    }


    IEnumerator LightActionTimer()
    {

        lightState.ChangeLightState(!startOn);

        yield return new WaitForSeconds(actionDuration);

        lightState.ChangeLightState(startOn);

        alreadyInteracted = false;

        StopCoroutine(coroutine);

    }

   
}
