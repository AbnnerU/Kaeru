using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingLight : MonoBehaviour
{
    //[SerializeField] private Detection detection;

    //[SerializeField] private Collider lightCollider;

    //[SerializeField] private Light theLight;

    [SerializeField] private LightState lightState;

    [SerializeField] private GameObject ligthEffect;

    [SerializeField] private Renderer render;

    [Header("Timers")]

    [SerializeField] private float timeOn;

    [SerializeField] private float timeOff;

    [SerializeField] private bool startOn;

    private Coroutine timerCoroutine;

    private Material material;

    private Color defaltMaterialColor;

    private void Awake()
    {
        if (render!=null)
        {
            material = render.material;
            defaltMaterialColor = material.GetColor("_EmissionColor");
        }
    }

   

    private void OnEnable()
    {
        if (startOn)
        {
            timerCoroutine = StartCoroutine(LightOnTimer());
        }
        else
        {
            timerCoroutine = StartCoroutine(LightOffTimer());
        }
    }

    IEnumerator LightOnTimer()
    {
        lightState.ChangeLightState(true);

        ligthEffect.SetActive(true);

        if (material)
        {
            material.SetColor("_EmissionColor", defaltMaterialColor);
        }
    
        yield return new WaitForSeconds(timeOn);

        StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(LightOffTimer());
    }

    IEnumerator LightOffTimer()
    {
        lightState.ChangeLightState(false);

        ligthEffect.SetActive(false);

        if (material)
        {
            material.SetColor("_EmissionColor", Color.black);
        }

        yield return new WaitForSeconds(timeOff);

        StopCoroutine(timerCoroutine);

        timerCoroutine = StartCoroutine(LightOnTimer());
    }

    //private void SetComponentsActive(bool active)
    //{
    //    lightState.ChangeLightState(active);     
    //}

}
