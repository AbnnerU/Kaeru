using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class OnOutOfLigth : UnityEvent<bool> { }

public class GhostOnLightActions : MonoBehaviour, IHasLightInterrations
{
    [SerializeField] private SwitchCharacter switchCharacter;

    [SerializeField] private float maxTimeOutOfLight;

    [SerializeField] private float newMaxSpeed;

    [SerializeField] private float newAceleration;

    [SerializeField] private float newFlyForce;

    [SerializeField] private List<LigthRayCast> lightOnRange;

    public OnOutOfLigth outOfLigth;

    private DefaltMovement defaltMovement;

    private CanvasManeger_Game canvasManeger;

    private void Awake()
    {
        canvasManeger = FindObjectOfType<CanvasManeger_Game>();
    }

        private void Start()
    {

        defaltMovement = GetComponent<DefaltMovement>();
    }

    private void OnEnable()
    {       
       
    }

    private void OnDisable()
    {
        lightOnRange.Clear();

        StopAllCoroutines();
    }    

    public void WhenLightRange(LigthRayCast lightRay)
    {
        if (canvasManeger != null)
        {
            canvasManeger.TimerAnimation(false, maxTimeOutOfLight);
        }

        outOfLigth?.Invoke(false);

        StopAllCoroutines();

        if (lightOnRange.Contains(lightRay) == false)
        {
            lightOnRange.Add(lightRay);

            defaltMovement.SetDefaltValues();
        }
    }

    public void WhenOutLightRange(LigthRayCast lightRay)
    {
        

        if (lightOnRange.Contains(lightRay) == true)
        {
            lightOnRange.Remove(lightRay);
        }

        if (lightOnRange.Count == 0)
        {
            if (canvasManeger != null)
            {
                canvasManeger.TimerAnimation(true, maxTimeOutOfLight);
            }

            StartCoroutine(OutOfLightTimer());
        }
    }

    public void StopExecutingCoroutaines()
    {
        if (canvasManeger != null)
        {
            canvasManeger.TimerAnimation(false, maxTimeOutOfLight);
        }

        StopAllCoroutines();

        lightOnRange.Clear();
    }

    IEnumerator OutOfLightTimer()
    {
        outOfLigth?.Invoke(true);

        defaltMovement.SetNewValues(newMaxSpeed,newAceleration,newFlyForce);

        yield return new WaitForSeconds(maxTimeOutOfLight);

        ChangeCharacter();

    }

    public void ChangeCharacter()
    {
        StopExecutingCoroutaines();

        switchCharacter.ChangeCharacter();

        
    }

   

}
