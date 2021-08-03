
using UnityEngine;

public class LightState : MonoBehaviour
{
    [SerializeField] private Collider lightCollider;

    [SerializeField] private Light theLight;

    [SerializeField] private Detection detection;

    [SerializeField] private bool justTurnOff = false;

    [SerializeField] private bool StartOn=true;


    private bool currentState;

    private void Awake()
    {
        currentState = StartOn;

        ChangeLightState(StartOn);
    }

    public void ChangeLightState(bool lightOn)
    {
        if (justTurnOff==false)
        {
            lightCollider.enabled = lightOn;

            theLight.enabled = lightOn;

            if (lightOn == false)
                detection.DetectionDisable();
        }
        else
        {
            theLight.enabled = lightOn;
        }
    }

    public void InvertLightState()
    {
        currentState = !currentState;


        lightCollider.enabled = currentState;

        theLight.enabled = currentState;

        if (currentState == false)
            detection.DetectionDisable();


    }
}
