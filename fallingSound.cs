using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable] public class OnTouch : UnityEvent { }

public class fallingSound : MonoBehaviour
{
    [SerializeField] private SmashDetection refDetection;
    [SerializeField] private SmashDetectionManeger smashDetectionManeger;

    public OnTouch onTouch;

    private bool alreadyPlay=true;

    private void Awake()
    {
        refDetection.OnDetectCollision += SmashDetection_OnDetectCollision;
    }

    private void SmashDetection_OnDetectCollision(bool collision)
    {
        if (collision && alreadyPlay==false)
        {
            alreadyPlay = true;

            if (smashDetectionManeger.GetCanVerify())
            {
                print("Rola");
                onTouch?.Invoke();
            }
        }
        else if(collision==false)
        {
            print("Ola");
            alreadyPlay = false;
        }
    }
}
